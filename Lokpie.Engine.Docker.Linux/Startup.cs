using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QSI.Automapper.Extension;
using QSI.Common.Api.AspNetCore.JsonConverters;
using QSI.MassTransit.Boot.Starter.AspNetCore;
using QSI.ORM.Config;
using QSI.ORM.NHibernate.Extension;
using QSI.Quartz.Extensions;
using QSI.Security.Api.AspNetCore.Extension;
using QSI.Swagger.Common;
using QSI.Swagger.Extensions;
using QSI.Web.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using QSI.FluentValidation.Interceptors;
using QSI.Security.Service.JWT;

namespace Lokpie.Engine.Docker.Linux
{
    /// <summary>
    /// Startup class that configure Apps DI, etc
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Load needed apps configuration on constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddYamlFile("configuration.yml", optional: false, reloadOnChange: true)
                .AddYamlFile($"configuration.{env.EnvironmentName}.yml", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// Apps configuration file
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Autofac container
        /// </summary>
        public ILifetimeScope AutofacContainer { get; private set; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region Response Compression & Caching
            services.AddResponseCaching();
            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Default",
                    new CacheProfile()
                    {
                        NoStore = true,
                        Location = ResponseCacheLocation.None,
                        Duration = 0
                    });
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddControllersAsServices()

                // FluentValidation config
                .AddFluentValidation(opts =>
                {
                    opts.AutomaticValidationEnabled = true;
                    opts.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    opts.ImplicitlyValidateChildProperties = true;
                    opts.ImplicitlyValidateRootCollectionElements = true;
                    Assembly[] assemblies = new Assembly[]
                    {
                        Assembly.Load("Lokpie.Common"),
                    };
                    opts.RegisterValidatorsFromAssemblies(assemblies);
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new DateTimeConverter());
                });
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);

            // FluentValidation Exception Shaper
            services.AddTransient<IValidatorInterceptor, ExceptionShaperInterceptor>();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
            });
            #endregion

            #region ORM Setup
            var setting = new DatabaseConfiguration();
            Configuration.Bind("orm", setting);
            services.AddConnection(setting);
            #endregion

            #region Swagger UI
            services.AddSwaggerUI(Configuration);
            #endregion

            #region JWT Authorization
            var jwtsetting = new JwtConfiguration();
            Configuration.Bind("jwt", jwtsetting);
            services.AddJwt(jwtsetting);
            #endregion

            #region Quartz Scheduler
            services.UseQuartz(Configuration);
            #endregion

            #region Masstransit BusService DI
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BusService>();
            #endregion

            #region Automapper config
            var mapper = Configuration.GetSection("automapper").Get<string[]>();
            services.AddAutoMapper(mapperConfig =>
            {
                mapperConfig.AddProfiles(mapper);
                mapperConfig.IgnoreUnmapped();
            });
            #endregion
        }

        /// <summary>
        /// Configure Dependency Injection from container builder
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterModule(new AutofacModule(Configuration));
        }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="appLifetime"></param>
        /// <param name="swaggerConfiguration"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory, IHostApplicationLifetime appLifetime,
            SwaggerConfiguration swaggerConfiguration, IApiVersionDescriptionProvider provider)
        {
            loggerFactory.AddLog4Net(Configuration.GetValue<string>("Log4NetConfigFile:Name"));

            // Forcing HTTPS Redirect
            // app.UseHsts();

            #region Response Sharper
            ResponseActionFactory factory = new ResponseActionFactory();
            factory.AddCase(async (e, c) =>
            {
                var ce = e as QSI.Common.Exception.Validation.ValidationException;
                if (ce != null)
                {
                    c.Response.ContentType = "application/json";
                    c.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var ex = new QSI.Web.Middleware.JsonException()
                    {
                        ErrorCode = ce.ErrorCode,
                        Message = ce.Message,
                        MessageKey = ce.MessageKey,
                        Exception = e.GetType().FullName,
                        Data = new Dictionary<string, object>
                        {
                                { "Failures", ce.Failures }
                        }
                    };

                    await c.Response.WriteAsync(JsonConvert.SerializeObject(ex));
                    return true;
                }
                return false;
            });
            app.UseMiddleware<MiddlewareExceptionShaper>(factory);
            #endregion

            #region CORS
            if (Configuration.GetSection("cors").Exists())
            {
                app.UseCors(builder =>
                {
                    var allowCredentials = Configuration.GetSection("cors:credentials").Get<bool>();
                    var origins = Configuration.GetSection("cors:origins").Get<string[]>();
                    var headers = Configuration.GetSection("cors:headers").Get<string[]>();
                    var methods = Configuration.GetSection("cors:methods").Get<string[]>();
                    builder.WithOrigins(origins).WithHeaders(headers).WithMethods(methods);
                    if (allowCredentials)
                    {
                        builder.AllowCredentials();
                    }
                    else
                    {
                        builder.DisallowCredentials();
                    }
                });
            }
            #endregion

            #region Swagger
            app.UseSwaggerUI(swaggerConfiguration, provider);
            #endregion

            app.UseResponseCaching();
            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        NoCache = true,
                        NoStore = true,
                        MaxAge = TimeSpan.Zero
                    };

                await next();
            });
            app.UseResponseCompression();
            app.UseSecurityHeaders();
            app.UseAuthentication();
            app.UseRouting();
#if NETCOREAPP3_0_OR_GREATER
            app.UseAuthorization();
#endif
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });


            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            appLifetime.ApplicationStopped.Register(() =>
            {
                if (this.AutofacContainer != null) this.AutofacContainer.Dispose();
            });
        }
    }
}

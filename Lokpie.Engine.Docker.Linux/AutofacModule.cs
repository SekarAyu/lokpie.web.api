using Autofac;
using MassTransit.NHibernateIntegration.Saga;
using MassTransit.RabbitMqTransport;
using MassTransit.Saga;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using QSI.MassTransit.Boot.Starter.Configurations;
using QSI.MassTransit.Boot.Starter.Extensions;
using QSI.ORM.NHibernate.Extension;
using QSI.Security.Api.AspNetCore.Config;
using QSI.Security.Api.AspNetCore.Extension;
using QSI.Security.Service.Authentication.Provider;
using System;
using System.Linq;
using System.Reflection;

namespace Lokpie.Engine.Docker.Linux
{
    /// <summary>
    /// Autofac module class, you can configure any additional builder container setup here
    /// </summary>
    public class AutofacModule : Autofac.Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public AutofacModule(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="ArgumentException"></exception>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            #region Transactional
            Assembly[] assemblies = new Assembly[]
            {
                Assembly.Load("QSI.Security.Service"),
                Assembly.Load("QSI.Security.Repository.NHibernate"),
                Assembly.Load("Lokpie.Service"),
                Assembly.Load("Lokpie.Repository.NHibernate"),
                Assembly.Load("QSI.Document.Service"),
                Assembly.Load("QSI.Document.Repository.NHibernate"),
                //Assembly.Load("Lokpie.Repository"),
            };
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            builder.RegisterTransactionalAssemblies(assemblies, e =>
            {
                var context = e.Resolve<IHttpContextAccessor>();
                if (context.HttpContext == null)
                    return "Unauthenticated";
                return context.HttpContext.User.Identity.Name;
            });
            builder.RegisterCriteriaCondition();
            builder.RegisterGeneralHelperDao();
            #endregion

            #region Authentication
            var loginConfiguration = new LoginConfiguration();
            loginConfiguration.SetAuthenticationProvider(typeof(DaoAuthenticationProvider));
            builder.AddDatabaseAuthentication(loginConfiguration);
            #endregion

            #region RabbitMQ MassTransit
            MassTransitBootConfiguration masstransitBootConfiguration = new MassTransitBootConfiguration();
            Configuration.Bind("masstransit", masstransitBootConfiguration);
            builder.RegisterGeneric(typeof(NHibernateSagaRepository<>)).As(typeof(ISagaRepository<>));
            builder.UseMassTransit(masstransitBootConfiguration, (cfg, host, ctx) =>
            {
                if (!cfg.GetType().GetInterfaces().Contains(typeof(IRabbitMqBusFactoryConfigurator)))
                {
                    throw new ArgumentException("Parameter host must implement IRabbitMqHost", "host");
                }
            });
            #endregion
        }
    }
}

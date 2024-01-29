# Lokpie NHibernate Starter Project
This repository contain starter project based on NHibernate Framework, using **.net 6.0**.

# Features

- 🐋 **Docker** containerized (Micro services ready)
- 🤖 **Autofac** Dependency Injection
- 📨 **RabbitMQ** MassTransit pre-configured
- 🕒 **Quartz** Scheduler Job pre-configured
- 👣 **Log4Net** based logging
- 🪄 **Automapper** pre-configured
- 🕶️ **CORS** pre-configured
- 🚀 Using latest custom ORM NHibernate Framework (**QSI.ORM**)
- 🐙 Micro services framework ready
- 📖 Include Swagger UI documentation
- 🎨 Include Swagger API Versioning

# CORS

By default CORS configuration will allow any origin, any header, and any method. To configure for specific domain, you can add or replace cors config on `configuration.yaml` file.

```yaml
# configuration.yaml
cors:
  credentials: true
  origins:
    - "http://mobile.dev.quadrant-si.id/cirrustavocadodev/"
    - "https://zurich-mzacms.dev.quadrant-si.id/"
  headers:
    - "*"
  methods:
    - "*"
```

# Docker Container Build
- To build docker image container, update `docker-compose.yml` file to fit your database server & masstransit configuration. Or you can create a `docker-compose.override.yml` file with content below:
```yaml
services:
  lokpie.engine.docker.linux:
    environment:
      - ASPNETCORE_ENVIRONMENT=Release
      - ASPNETCORE_URLS=http://+:80
      - orm__connection__connectionString=Server=quadrant.local,1433;Database=agentdev;User ID=dev;Password=123456
      - masstransit__masstransitConfiguration__baseUrl=rabbitmq://host.docker.internal:5672/qsi_vhost
    ports:
      - "5000:80"
    extra_hosts:
      - "host.docker.internal:host-gateway"
      - "quadrant.local:192.168.85.5"
```
- Please note that `host-gateway` is only available on Docker v20.10 and above, for Docker below that you need to specify docker host ip address (for more info please check [this SO Answer](https://stackoverflow.com/a/43541732))
- Run `docker-compose up --build` to run the container

# Date Time & Localization
- Please use `DateTime.UtcNow` instead `DateTime.Now` because by default DateTime object will be serialized to UTC ISO 8601 string format.
- Client / Consumer should parse the value from UTC Format to expected DateTime Time Zone offset
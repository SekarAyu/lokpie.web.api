ARG PROJECT="Lokpie.Engine.Docker.Linux"

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update && apt-get install -y --allow-unauthenticated libc6-dev libgdiplus libx11-dev && rm -rf /var/lib/apt/lists/*
ARG PROJECT
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
RUN apk add bash icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib --update-cache
RUN apk add libgdiplus --update-cache --repository http://dl-3.alpinelinux.org/alpine/edge/testing/ --allow-untrusted
WORKDIR /src
ARG PROJECT
COPY . .
RUN dotnet restore "${PROJECT}/${PROJECT}.csproj"
WORKDIR "/src/${PROJECT}"
RUN dotnet build ${PROJECT}.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ${PROJECT}.csproj -c Release -o /app

FROM base AS final
ENV PROJECTNAME "${PROJECT}.dll"
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT dotnet $PROJECTNAME


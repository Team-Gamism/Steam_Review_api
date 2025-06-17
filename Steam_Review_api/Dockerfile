FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
ARG APP_UID=1000
ENV APP_UID=$APP_UID
USER $APP_UID
WORKDIR /app
EXPOSE 5272
EXPOSE 7192

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Server/Server.csproj Server/

RUN dotnet restore Server/Server.csproj

COPY Server/. Server/

WORKDIR /src/Server

RUN dotnet build Server.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Server.csproj -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.dll"]

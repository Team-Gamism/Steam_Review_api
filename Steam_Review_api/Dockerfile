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

COPY ["Steam_Review_api/Steam_Review_api/Server/Server.csproj", "Server/"]

RUN dotnet restore "Server/Server.csproj"

COPY . .

WORKDIR "/src/Server"

RUN dotnet build "./Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet

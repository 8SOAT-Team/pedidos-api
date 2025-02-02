FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Pedidos.Domain/Pedidos.Domain.csproj", "src/Pedidos.Domain/"]

COPY ["src/Pedidos.Infrastructure/Pedidos.Infrastructure.csproj", "src/Pedidos.Infrastructure/"]
COPY ["src/Pedidos.CrossCutting/Pedidos.CrossCutting.csproj", "src/Pedidos.CrossCutting/"]
COPY ["src/Pedidos.Apps/Pedidos.Apps.csproj", "src/Pedidos.Apps/"]
COPY ["src/Pedidos.Api/Pedidos.Api.csproj", "src/Pedidos.Api/"]
COPY ["src/Pedidos.Adapters/Pedidos.Adapters.csproj", "src/Pedidos.Adapters/"]

RUN dotnet restore "./src/Pedidos.Api/Pedidos.Api.csproj"

COPY . .
WORKDIR "/src/src/Pedidos.Api"
RUN dotnet build "./Pedidos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Pedidos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pedidos.Api.dll"]
using Pedidos.Adapters.DependencyInjection;
using Pedidos.Api.Clientes;
using Pedidos.Api.Configurations;
using Pedidos.Api.Middlewares;
using Pedidos.Api.Pedidos;
using Pedidos.Api.Produtos.Endpoints;
using Pedidos.Api.Services;
using Pedidos.CrossCutting;
using Pedidos.Infrastructure.DependencyInjection;

DotNetEnv.Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureObservability()
    .ConfigureJsonSerialization()
    .ConfigureOpenApi()
    .ConfigureSecurity();

builder.Services
    .AddDatabaseContext()
    .AddCacheService()
    .AddGateways()
    .AddUseCaseControllers();

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseMiddleware<IdempotencyMiddleware>();

app.AddEndPointProdutos();
app.AddEndpointPedidos();
app.AddEndpointClientes();

app.ConfigureUseSwagger("FastOrder Pedidos API")
    .ConfigureUseSecurity()
    .ConfigureMapHealthChecks();

if (EnvConfig.RunMigrationsOnStart)
{
    await app.ExecuteMigrationAsync();
}

app.Run();
using Asp.Versioning;
using DotNetEnv;
using Pedidos.Adapters.DependencyInjection;
using Pedidos.Api.Clientes;
using Pedidos.Api.Configurations;
using Pedidos.Api.Endpoints;
using Pedidos.Api.Middlewares;
using Pedidos.Api.Pedidos;
using Pedidos.Api.Produtos.Endpoints;
using Pedidos.Api.Services;
using Pedidos.CrossCutting;
using Pedidos.Infrastructure.DependencyInjection;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5000);
});

builder.Services
    .ConfigureObservability()
    .ConfigureJsonSerialization()
    .ConfigureOpenApi()
    .ConfigureApiVersioning()
    .ConfigureSecurity();

builder.Services
    .AddDatabaseContext()
    .AddCacheService()
    .AddGateways()
    .AddUseCaseControllers();

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseMiddleware<IdempotencyMiddleware>();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

var group = app
    .MapGroup("v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.AddEndPointProdutos(group);
app.AddEndpointPedidos(group);
app.AddEndpointClientes(group);

app.ConfigureUseSwagger("FastOrder Pedidos API")
    .ConfigureUseSecurity()
    .ConfigureMapHealthChecks();

if (EnvConfig.RunMigrationsOnStart) await app.ExecuteMigrationAsync();

app.Run();

public partial class Program { }
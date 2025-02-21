using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pedidos.Adapters.Gateways.Pagamentos;
using Pedidos.Adapters.Gateways.Producao;
using Pedidos.Infrastructure.Databases;
using Pedidos.Tests.IntegrationTests.Fakes;
using Testcontainers.MsSql;

namespace Pedidos.Tests.IntegrationTests.HostTest;

public class FastOrderWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private IServiceScope? _scope;

    private readonly MsSqlContainer _mssqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    public FastOrderContext? Context { get; private set; }

    public async Task InitializeAsync()
    {
        await _mssqlContainer.StartAsync();
        _scope = Services.CreateScope();
        Context = _scope.ServiceProvider.GetRequiredService<FastOrderContext>();
    }

    public FastOrderWebApplicationFactory()
    {
        DotNetEnv.Env.TraversePath().Load();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IProducaoApi>();
            services.RemoveAll<IPagamentoApi>();

            services.AddSingleton<IProducaoApi, FakeProducaoApi>();
            services.AddSingleton<IPagamentoApi, FakePagamentoApi>();
            
            services.RemoveAll(typeof(DbContextOptions<FastOrderContext>));

            services.AddDbContext<FastOrderContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(_mssqlContainer.GetConnectionString()));
        });
    }

    public new async Task DisposeAsync()
    {
        if (_scope != null)
        {
            _scope.Dispose();
        }

        await _mssqlContainer.DisposeAsync();
    }
}
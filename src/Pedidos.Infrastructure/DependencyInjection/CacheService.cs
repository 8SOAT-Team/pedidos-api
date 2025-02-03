using Pedidos.Adapters.Gateways.Caches;
using Pedidos.CrossCutting;
using Pedidos.Infrastructure.Databases;
using StackExchange.Redis;

namespace Pedidos.Infrastructure.DependencyInjection;

public static class CacheService
{
    public static IServiceCollection AddCacheService(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(EnvConfig.DistributedCacheUrl, o =>
            {
                o.AbortOnConnectFail = false;
                o.ConnectRetry = 2;
                o.Ssl = false;
                o.ConnectTimeout = 5;
            }));
        services.AddSingleton<ICacheContext, CacheContext>();

        return services;
    }
}
﻿using Pedidos.Adapters.Gateways.Pagamentos;
using Pedidos.Apps.Clientes.Gateways;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.CrossCutting;
using Pedidos.Infrastructure.Clientes.Gateways;
using Pedidos.Infrastructure.Pagamentos.WebApis;
using Pedidos.Infrastructure.Pedidos.Gateways;
using Pedidos.Infrastructure.Produtos.Gateways;
using Pedidos.Infrastructure.Requests;
using Refit;

namespace Pedidos.Infrastructure.DependencyInjection;

public static class GatewayService
{
    public static IServiceCollection AddGateways(this IServiceCollection services)
    {
        services.AddScoped<IClienteGateway, ClienteGateway>()
            .DecorateIf<IClienteGateway, ClienteGatewayCache>(() => !EnvConfig.IsTestEnv);

        services.AddScoped<IProdutoGateway, ProdutoGateway>()
            .DecorateIf<IProdutoGateway, ProdutoGatewayCache>(() => !EnvConfig.IsTestEnv);

        services.AddScoped<IPedidoGateway, PedidoGateway>()
            .DecorateIf<IPedidoGateway, PedidoGatewayCache>(() => !EnvConfig.IsTestEnv);

        services.AddSingleton<IRequestGateway, RequestGateway>();

        services.AddScoped<IPagamentoGateway, PagamentoGateway>();

        services.AddRefitClient<IPagamentoWebApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(EnvConfig.PagamentoWebApiUrl));

        services.AddScoped<IPagamentoApi, PagamentoApi>();
        return services;
    }
}
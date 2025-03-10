﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Pedidos.Adapters.Controllers.Clientes;
using Pedidos.Adapters.Controllers.Pedidos;
using Pedidos.Adapters.Controllers.Produtos;
using Pedidos.Apps.Pedidos.EventHandlers;

namespace Pedidos.Adapters.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class Container
{
    public static IServiceCollection AddUseCaseControllers(this IServiceCollection services)
    {
        services.AddScoped<IClienteController, ClienteController>();
        services.AddScoped<IPedidoController, PedidoController>();
        services.AddScoped<IProdutoController, ProdutoController>();

        services.AddScoped<IPedidoHandler, PedidoHandler>();
        return services;
    }
}
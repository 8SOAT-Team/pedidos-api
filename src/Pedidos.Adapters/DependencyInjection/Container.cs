using Microsoft.Extensions.DependencyInjection;
using Pedidos.Adapters.Controllers.Clientes;
using Pedidos.Adapters.Controllers.Pedidos;
using Pedidos.Adapters.Controllers.Produtos;

namespace Pedidos.Adapters.DependencyInjection;

public static class Container
{
    public static IServiceCollection AddUseCaseControllers(this IServiceCollection services)
    {
        services.AddScoped<IClienteController, ClienteController>();
        services.AddScoped<IPedidoController, PedidoController>();
        services.AddScoped<IProdutoController, ProdutoController>();

        return services;
    }
}
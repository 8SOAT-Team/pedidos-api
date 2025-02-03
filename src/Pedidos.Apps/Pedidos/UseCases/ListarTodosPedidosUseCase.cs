using CleanArch.UseCase.Options;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.UseCases;
using Pedidos.Domain.Pedidos.Entities;

namespace Pedidos.Apps.Pedidos.UseCases;

public class ListarTodosPedidosUseCase(ILogger<ListarTodosPedidosUseCase> logger, IPedidoGateway pedidoGateway)
    : UseCase<ListarTodosPedidosUseCase, Any<object>, List<Pedido>>(logger)
{
    protected override async Task<List<Pedido>?> Execute(Any<object> command)
    {
        return await pedidoGateway.GetAllAsync();
    }
}
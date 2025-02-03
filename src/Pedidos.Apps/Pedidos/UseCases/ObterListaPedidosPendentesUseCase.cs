using CleanArch.UseCase.Options;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.UseCases;
using Pedidos.Domain.Pedidos.Entities;

namespace Pedidos.Apps.Pedidos.UseCases;

public class ObterListaPedidosPendentesUseCase(
    ILogger<ObterListaPedidosPendentesUseCase> logger,
    IPedidoGateway pedidoGateway)
    : UseCase<ObterListaPedidosPendentesUseCase, Any<object>, List<Pedido>>(logger)
{
    protected override async Task<List<Pedido>?> Execute(Any<object> command)
    {
        return await pedidoGateway.GetAllPedidosPending();
    }
}
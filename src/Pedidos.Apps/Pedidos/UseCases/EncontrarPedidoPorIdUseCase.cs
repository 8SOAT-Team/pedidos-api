using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.UseCases;
using Pedidos.Domain.Pedidos.Entities;

namespace Pedidos.Apps.Pedidos.UseCases;

public class EncontrarPedidoPorIdUseCase(ILogger<EncontrarPedidoPorIdUseCase> logger, IPedidoGateway pedidoGateway)
    : UseCase<EncontrarPedidoPorIdUseCase, Guid, Pedido>(logger)
{
    protected override async Task<Pedido?> Execute(Guid pedidoId)
    {
        return await pedidoGateway.GetByIdAsync(pedidoId);
    }
}
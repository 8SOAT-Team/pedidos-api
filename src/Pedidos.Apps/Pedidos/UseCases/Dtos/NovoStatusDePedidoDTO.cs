using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Apps.Pedidos.UseCases.Dtos;

public record NovoStatusDePedidoDto
{
    public Guid PedidoId { get; init; }
    public StatusPedido NovoStatus { get; init; }
}
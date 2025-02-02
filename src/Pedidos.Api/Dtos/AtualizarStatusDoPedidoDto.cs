using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Api.Dtos;

public record AtualizarStatusDoPedidoDto
{
    public StatusPedido NovoStatus { get; init; }
}
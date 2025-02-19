using Pedidos.Domain.Pedidos.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Pedidos.Adapters.Gateways.Pedido.Dtos;

[ExcludeFromCodeCoverage]
public record PedidoCriadoDto
{
    public Guid Id { get; init; }
    public DateTime DataPedido { get; init; }
    public StatusPedido StatusPedido { get; init; }
    public decimal ValorTotal { get; init; }
}

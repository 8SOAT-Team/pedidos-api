using System.Diagnostics.CodeAnalysis;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Adapters.Gateways.Pedidos.Dtos;

[ExcludeFromCodeCoverage]
public record PedidoCriadoDto
{
    public Guid Id { get; init; }
    public DateTime DataPedido { get; init; }
    public StatusPedido StatusPedido { get; init; }
    public decimal ValorTotal { get; init; }
}

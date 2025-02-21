using System.Diagnostics.CodeAnalysis;

namespace Pedidos.Apps.Pedidos.UseCases.Dtos;
[ExcludeFromCodeCoverage]
public record NovoPedidoDto
{
    public Guid? ClienteId { get; init; }
    public List<ItemDoPedidoDto> ItensDoPedido { get; init; } = null!;
}
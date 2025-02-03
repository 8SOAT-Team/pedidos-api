namespace Pedidos.Apps.Pedidos.UseCases.Dtos;

public record NovoPedidoDto
{
    public Guid? ClienteId { get; init; }
    public List<ItemDoPedidoDto> ItensDoPedido { get; init; } = null!;
}
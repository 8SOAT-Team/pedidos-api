namespace Pedidos.Apps.Pedidos.UseCases.Dtos;

public record ItemDoPedidoDto
{
    public Guid Id { get; init; }
    public Guid ProdutoId { get; init; }
    public int Quantidade { get; init; }
}
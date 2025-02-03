namespace Pedidos.Adapters.Controllers.Pedidos.Dtos;

public record ItemDoPedidoDto
{
    public Guid Id { get; init; }
    public Guid ProdutoId { get; init; }
    public int Quantidade { get; init; }
    public string Imagem { get; init; } = null!;
}
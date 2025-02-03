namespace Pedidos.Adapters.Controllers.Pedidos.Dtos;

public record NovoPedidoDto
{
    public Guid? ClienteId { get; init; }
    public List<NovoItemDePedido> ItensDoPedido { get; init; } = null!;
}

public record NovoItemDePedido
{
    public Guid ProdutoId { get; init; }
    public int Quantidade { get; init; }
}
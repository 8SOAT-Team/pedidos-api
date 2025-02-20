using System.Diagnostics.CodeAnalysis;

namespace Pedidos.Adapters.Gateways.Pedido.Dtos;
[ExcludeFromCodeCoverage]
public record NovoPedidoDto
{
    public Guid? PedidoId { get; init; }
    public List<NovoItemDePedido> ItensDoPedido { get; init; } = null!;
}

[ExcludeFromCodeCoverage]
public record NovoItemDePedido
{
    public Guid ProdutoId { get; init; }
    public int Quantidade { get; init; }
}
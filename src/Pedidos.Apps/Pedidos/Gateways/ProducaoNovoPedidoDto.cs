using System.Diagnostics.CodeAnalysis;

namespace Pedidos.Apps.Pedidos.Gateways;

[ExcludeFromCodeCoverage]
public record ProducaoNovoPedidoDto
{
    public Guid PedidoId { get; init; }
    public List<ProducaoNovoItemDePedidoRequest> ItensDoPedido { get; init; } = null!;
}

[ExcludeFromCodeCoverage]
public record ProducaoNovoItemDePedidoRequest
{
    public Guid ProdutoId { get; init; }
    public int Quantidade { get; init; }
}
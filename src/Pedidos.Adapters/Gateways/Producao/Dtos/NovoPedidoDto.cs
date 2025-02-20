using Pedidos.Domain.Pedidos.Enums;
using System.Diagnostics.CodeAnalysis;
using Pedidos.Apps.Produtos.Enums;

namespace Pedidos.Adapters.Gateways.Producao.Dtos;

[ExcludeFromCodeCoverage]
public record ProducaoNovoPedidoDto
{
    public Guid PedidoId { get; init; }
    public List<NovoItemDePedidoRequest> ItensDoPedido { get; init; } = null!;
}

[ExcludeFromCodeCoverage]
public record NovoPedidoRequest
{
    public Guid PedidoId { get; init; }
    public List<NovoItemDePedidoRequest> ItensDoPedido { get; init; } = null!;
}

[ExcludeFromCodeCoverage]
public record NovoItemDePedidoRequest
{
    public string Nome { get; init; }
    public ProdutoCategoria Categoria { get; set; }
    public int Quantidade { get; init; }
}

[ExcludeFromCodeCoverage]
public record PedidoResponse
{
    public Guid Id { get; init; }
    public DateTime DataPedido { get; init; }

    public StatusPedido StatusPedido { get; init; }
   
    public virtual IReadOnlyCollection<ItemDoPedidoResponse> ItensDoPedido { get; init; } = Array.Empty<ItemDoPedidoResponse>();

    public decimal ValorTotal { get; init; }
   
}

[ExcludeFromCodeCoverage]
public record ItemDoPedidoResponse
{
    public Guid Id { get; init; }
    public Guid ProdutoId { get; init; }
    public int Quantidade { get; init; }
    public string Imagem { get; init; } = null!;
}
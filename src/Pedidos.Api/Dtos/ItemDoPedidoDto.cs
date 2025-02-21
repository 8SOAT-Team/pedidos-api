using System.Diagnostics.CodeAnalysis;

namespace Pedidos.Api.Dtos;
[ExcludeFromCodeCoverage]
public record ItemDoPedidoDto
{
    public Guid Id { get; private set; }
    public Guid ProdutoId { get; init; }
    public int Quantidade { get; init; }

    public void SetId(Guid id)
    {
        Id = id;
    }
}
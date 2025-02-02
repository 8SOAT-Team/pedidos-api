using Pedidos.Apps.Produtos.Enums;

namespace Pedidos.Apps.Produtos.UseCases.DTOs;

public record ProdutoDto
{
    public Guid Id { get; init; }
    public string Nome { get; init; } = null!;
    public string Descricao { get; init; } = null!;
    public decimal Preco { get; init; }
    public string Imagem { get; init; } = null!;
    public ProdutoCategoria Categoria { get; init; }
}
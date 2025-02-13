using Pedidos.Apps.Produtos.Enums;
using Pedidos.Apps.Produtos.UseCases.DTOs;
using Pedidos.Domain.Produtos.Entities;

namespace Pedidos.Adapters.Presenters.Produtos;

public static class ProdutoPresenter
{
    public static ProdutoCriadoDto AdaptProdutoCriado(Produto produto)
    {
        return new ProdutoCriadoDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            Imagem = produto.Imagem,
            Categoria = (ProdutoCategoria)produto.Categoria
        };
    }

    public static ICollection<ProdutoDto> AdaptProduto(ICollection<Produto> produtos)
    {
        return produtos.Select(AdaptProduto).ToList();
    }

    public static ProdutoDto AdaptProduto(Produto produto)
    {
        return new ProdutoDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            Imagem = produto.Imagem,
            Categoria = (ProdutoCategoria)produto.Categoria
        };
    }
}
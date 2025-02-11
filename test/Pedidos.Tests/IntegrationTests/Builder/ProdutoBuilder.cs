using Bogus;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Domain.Produtos.Enums;

namespace Pedidos.Tests.IntegrationTests.Builder;

public sealed class ProdutoBuilder : Faker<Produto>
{
    public ProdutoBuilder()
    {
        CustomInstantiator(f => new Produto(nome: f.Commerce.ProductName(),
            descricao: f.Commerce.ProductDescription().Substring(0, 30),
            preco: decimal.Parse(f.Commerce.Price(1, 1000)), imagem: f.Image.LoremFlickrUrl()!,
            categoria: f.PickRandom<ProdutoCategoria>()
            ));
    }

    public Produto Build() => Generate();
}
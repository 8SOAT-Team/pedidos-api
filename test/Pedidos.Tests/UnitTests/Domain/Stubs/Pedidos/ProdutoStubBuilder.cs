using Bogus;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Domain.Produtos.Enums;

namespace Pedidos.Tests.UnitTests.Domain.Stubs.Pedidos;

internal sealed class ProdutoStubBuilder : Faker<Produto>
{
    private ProdutoStubBuilder()
    {
        CustomInstantiator(f => new Produto(f.Commerce.ProductName(), f.Commerce.ProductName(), f.Random.Int(1, 1000),
            "http://fast-order-imagens.biz/produto-imagem", f.PickRandom<ProdutoCategoria>()));

        RuleFor(x => x.Imagem, "http://fast-order-imagens.biz/produto-imagem");
    }

    public static Produto Create() => new ProdutoStubBuilder().Generate();
    public static List<Produto> CreateMany(int qty) => new ProdutoStubBuilder().Generate(qty);
}
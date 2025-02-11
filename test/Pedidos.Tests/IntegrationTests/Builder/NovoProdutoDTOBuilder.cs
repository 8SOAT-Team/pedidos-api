using Bogus;
using Pedidos.Apps.Produtos.Enums;
using Pedidos.Apps.Produtos.UseCases.DTOs;

namespace Pedidos.Tests.IntegrationTests.Builder;
public class NovoProdutoDtoBuilder : Faker<NovoProdutoDto>
{
    public NovoProdutoDtoBuilder()
    {
        CustomInstantiator(f => new NovoProdutoDto()
        {
            Nome = f.Commerce.ProductName().Substring(0, 10),
            Descricao = f.Commerce.ProductDescription().Substring(0, 10),
            Preco = f.Random.Decimal(5, 1000),
            Imagem = f.Image.LoremFlickrUrl(),
            Categoria = f.PickRandom<ProdutoCategoria>()
        });
    }
    public NovoProdutoDto Build() => Generate();


}

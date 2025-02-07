using Bogus;
using Pedidos.Adapters.Controllers.Pedidos.Dtos;
using Postech8SOAT.FastOrder.Tests.Integration.Builder;

namespace Pedidos.Tests.IntegrationTests.Builder;
internal class NovoItemDePedidoBuilder : Faker<NovoItemDePedido>
{
    public NovoItemDePedidoBuilder()
    {
        CustomInstantiator(f => new NovoItemDePedido()
        {
            ProdutoId = RetornaIdProdutoUtil.RetornaIdProduto(),
            Quantidade = f.Random.Int(1, 10)
        });
    }

    public NovoItemDePedido Build() => Generate();
}

using Bogus;
using Pedidos.Adapters.Controllers.Pedidos.Dtos;

namespace Pedidos.Tests.IntegrationTests.Builder;
internal class NovoPedidoDtoBuilder : Faker<NovoPedidoDto>
{
    public NovoPedidoDtoBuilder()
    {
        CustomInstantiator(f => new NovoPedidoDto()
        {
            ClienteId = f.Random.Guid(),
            ItensDoPedido = new List<NovoItemDePedido>()
            {
                new NovoItemDePedidoBuilder().Build(),
                new NovoItemDePedidoBuilder().Build()
            }
        });
    }

    public NovoPedidoDtoBuilder(Guid clienteId)
    {
        CustomInstantiator(f => new NovoPedidoDto()
        {
            ClienteId = clienteId,
            ItensDoPedido = new List<NovoItemDePedido>()
            {
                new NovoItemDePedidoBuilder().Build(),
                new NovoItemDePedidoBuilder().Build()
            }
        });
    }

    public NovoPedidoDto Build() => Generate();
}



using Bogus;
using Pedidos.Adapters.Controllers.Pedidos.Dtos;

namespace Pedidos.Tests.IntegrationTests.Builder;
internal class NovoPedidoDtoInvalidoBuilder : Faker<NovoPedidoDto>
{
    public NovoPedidoDtoInvalidoBuilder()
    {
        CustomInstantiator(f => new NovoPedidoDto()
        {
            ClienteId = Guid.Empty,
            ItensDoPedido = new List<NovoItemDePedido>()
            {
                new NovoItemDePedidoBuilder().Build(),
                new NovoItemDePedidoBuilder().Build()
            }
        });
    }
    public NovoPedidoDto Build() => Generate();
}

using Bogus;
using Pedidos.Api.Dtos;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Tests.IntegrationTests.Builder;
internal class AtualizarStatusDoPedidoDtoBuilder : Faker<AtualizarStatusDoPedidoDto>
{
    public AtualizarStatusDoPedidoDtoBuilder()
    {
        CustomInstantiator(f => new AtualizarStatusDoPedidoDto()
        {
            NovoStatus = StatusPedido.EmPreparacao
        });
    }
    public AtualizarStatusDoPedidoDto Build() => Generate();
}

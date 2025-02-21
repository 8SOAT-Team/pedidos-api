using Bogus;
using Pedidos.Adapters.Gateways.Pagamentos;
using Pedidos.Adapters.Gateways.Pagamentos.Dtos;
using Pedidos.Adapters.Gateways.Producao;
using Pedidos.Adapters.Gateways.Producao.Dtos;
using Pedidos.Adapters.Gateways.WebApis;

namespace Pedidos.Tests.IntegrationTests.Fakes;

public class FakeProducaoApi : IProducaoApi
{
    private static readonly Faker _faker = new();

    public Task<ApiResponse<PedidoResponse>> IniciarProducaoAsync(ProducaoNovoPedidoDto dto)
    {
        return Task.FromResult(new ApiResponse<PedidoResponse>
        {
            Content = new PedidoResponse
            {
                Id = Guid.NewGuid(),
                DataPedido = DateTime.Now,
            }
        });
    }
}
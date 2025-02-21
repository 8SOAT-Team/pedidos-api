using Bogus;
using Pedidos.Adapters.Gateways.Pagamentos;
using Pedidos.Adapters.Gateways.Pagamentos.Dtos;
using Pedidos.Adapters.Gateways.WebApis;

namespace Pedidos.Tests.IntegrationTests.Fakes;

public class FakePagamentoApi : IPagamentoApi
{
    private static readonly Faker _faker = new();

    public Task<ApiResponse<PagamentoCriadoDto>> IniciarPagamentoAsync(NovoPagamentoDto dto)
    {
        return Task.FromResult(new ApiResponse<PagamentoCriadoDto>
        {
            Content = new PagamentoCriadoDto
            {
                Id = Guid.NewGuid(),
                Status = 0,
                UrlPagamento = _faker.Internet.Url(),
            }
        });
    }
}
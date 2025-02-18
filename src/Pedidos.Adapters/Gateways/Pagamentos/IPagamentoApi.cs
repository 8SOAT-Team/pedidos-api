using Pedidos.Adapters.Gateways.Pagamentos.Dtos;
using Pedidos.Adapters.Gateways.WebApis;

namespace Pedidos.Adapters.Gateways.Pagamentos;

public interface IPagamentoApi
{
    Task<ApiResponse<PagamentoCriadoDto>> IniciarPagamentoAsync(NovoPagamentoDto dto);
}
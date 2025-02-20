using Pedidos.Adapters.Gateways.Producao.Dtos;
using Pedidos.Adapters.Gateways.WebApis;

namespace Pedidos.Adapters.Gateways.Producao;

public interface IProducaoApi
{
    Task<ApiResponse<PedidoResponse>> IniciarProducaoAsync(NovoPedidoDto dto);
}
using Pedidos.Adapters.Gateways.Pedido.Dtos;
using Pedidos.Adapters.Gateways.WebApis;

namespace Pedidos.Adapters.Gateways.Producao;

public interface IProducaoApi
{
    Task<ApiResponse<PedidoCriadoDto>> IniciarProducaoAsync(NovoPedidoDto dto);
}
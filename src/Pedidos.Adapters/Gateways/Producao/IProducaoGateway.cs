using Pedidos.Adapters.Gateways.Producao.Dtos;
using Pedidos.Adapters.Gateways.WebApis;

namespace Pedidos.Apps.Producoes.Gateways;
public interface IProducaoGateway
{
    Task<ApiResponse<PedidoResponse>> IniciarProducaoAsync(Guid pedidoId);
}


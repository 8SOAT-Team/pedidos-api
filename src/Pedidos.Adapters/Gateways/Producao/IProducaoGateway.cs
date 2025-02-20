using Pedidos.Adapters.Gateways.Producao.Dtos;
using Pedidos.Adapters.Gateways.WebApis;
using Pedidos.Domain.Pedidos.Entities;

namespace Pedidos.Adapters.Gateways.Producao;
public interface IProducaoGateway
{
    Task<ApiResponse<PedidoResponse>> IniciarProducaoAsync(Pedido pedido);
}


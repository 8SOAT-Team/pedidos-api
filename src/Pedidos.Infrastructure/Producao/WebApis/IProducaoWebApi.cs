using Pedidos.Adapters.Gateways.Producao.Dtos;
using Refit;

namespace Pedidos.Infrastructure.Producao.WebApis;
public interface IProducaoWebApi
{
    [Post("/v1/pedido")]
    public Task<ApiResponse<PedidoResponse>> IniciarProducao(NovoPedidoRequest request);
}

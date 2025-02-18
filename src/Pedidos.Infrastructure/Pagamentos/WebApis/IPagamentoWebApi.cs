using Pedidos.Infrastructure.Pagamentos.WebApis.Dtos;
using Refit;

namespace Pedidos.Infrastructure.Pagamentos.WebApis;

public interface IPagamentoWebApi
{
    [Post("/pagamento/pedido/{pedidoId}")]
    public Task<ApiResponse<PagamentoResponse>> IniciarPagamento(Guid pedidoId, [Body] NovoPagamentoRequest request);
}
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Types.Results;

namespace Pedidos.Adapters.Gateways.Pagamentos;

public interface IPagamentoGateway
{
    public Task<Result<Pedidos.Domain.Pedidos.Entities.Pedido>> IniciarPagamentoAsync(NovoPagamentoDto dto, Pedidos.Domain.Pedidos.Entities.Pedido pedido);
}
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Types.Results;
using Pedidos.Domain.Pedidos.Entities;

namespace Pedidos.Adapters.Gateways.Pagamentos;

public interface IPagamentoGateway
{
    public Task<Result<Pedido>> IniciarPagamentoAsync(NovoPagamentoDto dto, Pedido pedido);
}
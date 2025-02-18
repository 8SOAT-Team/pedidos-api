using Pedidos.Adapters.Gateways.Pagamentos.Dtos;
using Pedidos.Adapters.Types.Results;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Pedidos.ValueObjects;

namespace Pedidos.Adapters.Gateways.Pagamentos;

public interface IPagamentoGateway
{
    public Task<Result<Pagamento>> IniciarPagamentoAsync(NovoPagamentoDto dto);
}
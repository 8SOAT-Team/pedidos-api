using System.Net;
using Pedidos.Adapters.Gateways.Pagamentos.Dtos;
using Pedidos.Adapters.Types.Results;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Pedidos.ValueObjects;

namespace Pedidos.Adapters.Gateways.Pagamentos;

public class PagamentoGateway(IPagamentoApi api, IPedidoGateway pedidoGateway) : IPagamentoGateway
{
    public async Task<Result<Pagamento>> IniciarPagamentoAsync(NovoPagamentoDto dto)
    {
        var pagamentoCriado = await api.IniciarPagamentoAsync(dto);

        if (pagamentoCriado.IsSuccessStatusCode is false)
        {
            return Result<Pagamento>.Failure(new AppProblemDetails("Erro ao iniciar pagamento",
                HttpStatusCode.InternalServerError.ToString(), pagamentoCriado.Error.Message, dto.PedidoId.ToString()));
        }

        var pedido = await pedidoGateway.GetByIdAsync(dto.PedidoId);

        pedido.PagamentoCriado(pagamentoCriado.Content.Id, pagamentoCriado.Content.UrlPagamento);

        pedido = await pedidoGateway.UpdateAsync(pedido);

        return Result<Pagamento>.Succeed(pedido.Pagamento);
    }
}
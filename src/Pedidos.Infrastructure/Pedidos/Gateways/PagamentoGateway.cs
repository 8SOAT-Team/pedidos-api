using System.Net;
using Pedidos.Adapters.Gateways.Pagamentos;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Types.Results;
using Pedidos.Domain.Pedidos.Entities;
using NovoPagamento = Pedidos.Adapters.Gateways.Pagamentos.Dtos.NovoPagamentoDto;

namespace Pedidos.Infrastructure.Pedidos.Gateways;

public class PagamentoGateway(IPagamentoApi api) : IPagamentoGateway
{
    public async Task<Result<Pedido>> IniciarPagamentoAsync(NovoPagamentoDto dto, Pedido pedido)
    {
        var pagamentoCriado = await api.IniciarPagamentoAsync(new NovoPagamento
        {
            PedidoId = dto.PedidoId,
            EmailPagador = dto.EmailPagador,
            ValorTotal = dto.ValorTotal
        });

        if (pagamentoCriado.IsSuccessStatusCode is false)
        {
            return Result<Pedido>.Failure(new AppProblemDetails("Erro ao iniciar pagamento",
                HttpStatusCode.InternalServerError.ToString(), pagamentoCriado.Error.Message, dto.PedidoId.ToString()));
        }

        pedido.PagamentoCriado(pagamentoCriado.Content.Id, pagamentoCriado.Content.UrlPagamento);

        return Result<Pedido>.Succeed(pedido);
    }
}
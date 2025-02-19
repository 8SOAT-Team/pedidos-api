using System.Net;
using Pedidos.Adapters.Gateways.Pagamentos;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Types.Results;
using Pedidos.Domain.Pedidos.Entities;
using MetodosDePagamento = Pedidos.Adapters.Gateways.Pagamentos.Enums.MetodosDePagamento;
using NovoPagamento = Pedidos.Adapters.Gateways.Pagamentos.Dtos.NovoPagamentoDto;
using NovoPagamentoPagadorRequest = Pedidos.Adapters.Gateways.Pagamentos.Dtos.NovoPagamentoPagadorRequest;
using NovoPagamentoItemRequest = Pedidos.Adapters.Gateways.Pagamentos.Dtos.NovoPagamentoItemRequest;

namespace Pedidos.Infrastructure.Pedidos.Gateways;

public class PagamentoGateway(IPagamentoApi api) : IPagamentoGateway
{
    public async Task<Result<Pedido>> IniciarPagamentoAsync(NovoPagamentoDto dto, Pedido pedido)
    {
        var pagamentoCriado = await api.IniciarPagamentoAsync(new NovoPagamento
        {
            PedidoId = dto.PedidoId,
            MetodoDePagamento = (MetodosDePagamento)dto.MetodoDePagamento,
            Pagador = dto.Pagador is not null
                ? new NovoPagamentoPagadorRequest()
                {
                    Email = dto.Pagador.Email,
                    Nome = dto.Pagador.Nome,
                    Cpf = dto.Pagador.Cpf
                }
                : null,
            Itens = dto.Itens.Select(x => new NovoPagamentoItemRequest
            {
                Id = x.Id,
                Titulo = x.Titulo,
                Descricao = x.Descricao,
                Quantidade = x.Quantidade,
                PrecoUnitario = x.PrecoUnitario,
            }).ToList()
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
using System.Diagnostics.CodeAnalysis;
using Pedidos.Adapters.Gateways.Pagamentos;
using Pedidos.Adapters.Gateways.Pagamentos.Dtos;
using Pedidos.Adapters.Gateways.Pagamentos.Enums;
using Pedidos.Adapters.Gateways.WebApis;
using Pedidos.Infrastructure.Pagamentos.WebApis.Dtos;
using NovoPagamentoItemRequest = Pedidos.Infrastructure.Pagamentos.WebApis.Dtos.NovoPagamentoItemRequest;
using NovoPagamentoPagadorRequest = Pedidos.Infrastructure.Pagamentos.WebApis.Dtos.NovoPagamentoPagadorRequest;

namespace Pedidos.Infrastructure.Pagamentos.WebApis;
[ExcludeFromCodeCoverage]
public class PagamentoApi(IPagamentoWebApi pagamentoWebApi) : IPagamentoApi
{
    public async Task<ApiResponse<PagamentoCriadoDto>> IniciarPagamentoAsync(NovoPagamentoDto dto)
    {
        var request = new NovoPagamentoRequest
        {
            MetodoDePagamento = MetodosDePagamento.Pix,
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
        };

        var response = await pagamentoWebApi.IniciarPagamento(dto.PedidoId, request);

        if (response.IsSuccessStatusCode is false)
        {
            throw response.Error;
        }

        return new ApiResponse<PagamentoCriadoDto>
        {
            StatusCode = response.StatusCode,
            Content = new PagamentoCriadoDto
            {
                Id = response.Content!.Id,
                Status = response.Content.Status,
                UrlPagamento = response.Content.UrlPagamento,
            }
        };
    }
}
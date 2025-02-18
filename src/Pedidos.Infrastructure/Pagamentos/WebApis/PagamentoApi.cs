using Pedidos.Adapters.Gateways.Pagamentos;
using Pedidos.Adapters.Gateways.Pagamentos.Dtos;
using Pedidos.Adapters.Gateways.Pagamentos.Enums;
using Pedidos.Adapters.Gateways.WebApis;
using Pedidos.Infrastructure.Pagamentos.WebApis.Dtos;

namespace Pedidos.Infrastructure.Pagamentos.WebApis;

public class PagamentoApi(IPagamentoWebApi pagamentoWebApi) : IPagamentoApi
{
    public async Task<ApiResponse<PagamentoCriadoDto>> IniciarPagamentoAsync(NovoPagamentoDto dto)
    {
        var request = new NovoPagamentoRequest
        {
            MetodoDePagamento = MetodosDePagamento.Cartao,
            EmailPagador = dto.EmailPagador,
            ValorTotal = dto.ValorTotal
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
using Pedidos.Adapters.Gateways.Producao;
using Pedidos.Adapters.Gateways.Producao.Dtos;
using Pedidos.Adapters.Gateways.WebApis;


namespace Pedidos.Infrastructure.Producao.WebApis;

public class ProducaoWebApi(IProducaoWebApi producaoApi) : IProducaoApi
{  
    public async Task<ApiResponse<PedidoResponse>> IniciarProducaoAsync(ProducaoNovoPedidoDto dto)
    {
        var request = new NovoPedidoRequest
        {
            PedidoId = dto.PedidoId,
            ItensDoPedido = dto.ItensDoPedido.Select(i => new NovoItemDePedidoRequest
            {
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade
            }).ToList()
        };

        var response = await producaoApi.IniciarProducao(request);

        if (response.IsSuccessStatusCode is false)
        {
            throw response.Error;
        }

        return new ApiResponse<PedidoResponse>
        {
            StatusCode = response.StatusCode,
            Content = new PedidoResponse
            {
                Id = response.Content!.Id,
                DataPedido = response.Content.DataPedido,
                StatusPedido = response.Content.StatusPedido,
                ValorTotal = response.Content.ValorTotal
            }
        };
    }
}

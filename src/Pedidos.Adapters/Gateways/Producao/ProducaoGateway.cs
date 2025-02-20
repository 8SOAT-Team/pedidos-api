using Pedidos.Adapters.Gateways.Producao.Dtos;
using Pedidos.Adapters.Gateways.WebApis;
using Pedidos.Domain.Pedidos.Entities;
using ProducaoNovoPedidoDto = Pedidos.Adapters.Gateways.Producao.Dtos.ProducaoNovoPedidoDto;


namespace Pedidos.Adapters.Gateways.Producao;

public class ProducaoGateway(IProducaoApi producaoApi) : IProducaoGateway
{
    public async Task<ApiResponse<PedidoResponse>> IniciarProducaoAsync(Pedido pedido)
    {
        var novoPedidoDto = new ProducaoNovoPedidoDto
        {
            PedidoId = pedido.Id,
            ItensDoPedido = pedido.ItensDoPedido.Select(i => new NovoItemDePedidoRequest
            {
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade
            }).ToList()
        };
        var response = await producaoApi.IniciarProducaoAsync(novoPedidoDto);

        return new ApiResponse<PedidoResponse>
        {
            StatusCode = response.StatusCode,
            Content = new PedidoResponse
            {
                Id = response.Content!.Id,
                StatusPedido = response.Content.StatusPedido,
                DataPedido = response.Content.DataPedido,
                ValorTotal = response.Content.ValorTotal
            }
        };
    }
}
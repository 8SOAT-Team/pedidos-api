using Pedidos.Adapters.Gateways.Producao.Dtos;
using Pedidos.Adapters.Gateways.WebApis;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Producoes.Gateways;


namespace Pedidos.Adapters.Gateways.Producao;
public class ProducaoGateway: IProducaoGateway
{
    private readonly IProducaoApi _producaoApi;
    private readonly IPedidoGateway _pedidoGateway;

    public ProducaoGateway(IProducaoApi producaoApi, IPedidoGateway pedidoGateway)
    {
        _producaoApi = producaoApi;
        _pedidoGateway = pedidoGateway;
    }

    public async Task<ApiResponse<PedidoResponse>> IniciarProducaoAsync(Guid pedidoId)
    {
        var pedido = await _pedidoGateway.GetPedidoCompletoAsync(pedidoId);
        var novoPedidoDto = new NovoPedidoDto
        {
             PedidoId= pedido!.Id,
            ItensDoPedido = pedido.ItensDoPedido.Select(i => new NovoItemDePedidoRequest
            {
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade
            }).ToList()
        };
        var response= await _producaoApi.IniciarProducaoAsync(novoPedidoDto);

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


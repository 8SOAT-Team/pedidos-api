using Pedidos.Adapters.Gateways.Pedido.Dtos;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Producoes.Gateway;

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

    public async Task IniciarProducaoAsync(Guid pedidoId)
    {
        var pedido = await _pedidoGateway.GetPedidoCompletoAsync(pedidoId);
        var novoPedidoDto = new NovoPedidoDto
        {
             PedidoId= pedido!.Id,
            ItensDoPedido = pedido.ItensDoPedido.Select(i => new NovoItemDePedido
            {
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade
            }).ToList()
        };
        await _producaoApi.IniciarProducaoAsync(novoPedidoDto);
    }


}


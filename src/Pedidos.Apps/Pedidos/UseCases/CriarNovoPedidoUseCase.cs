using CleanArch.UseCase.Faults;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Pedidos.UseCases.Dtos;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Apps.UseCases;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Produtos.Entities;

namespace Pedidos.Apps.Pedidos.UseCases;

public class CriarNovoPedidoUseCase(
    ILogger<CriarNovoPedidoUseCase> logger,
    IPedidoGateway pedidoGateway,
    IProdutoGateway produtoGateway) : UseCase<CriarNovoPedidoUseCase, NovoPedidoDto, Pedido>(logger)
{
    protected override async Task<Pedido?> Execute(NovoPedidoDto command)
    {
        var productsIds = command.ItensDoPedido.Select(i => i.ProdutoId);
        var productsEntityList = (await produtoGateway.ListarProdutosByIdAsync(productsIds.ToArray())).ToList();
        var missingProducts = productsIds.Except(productsEntityList.Select(p => p.Id)).ToArray();

        if (missingProducts.Length > 0)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest,
                $"Produto não encontrado: {string.Join(", ", missingProducts)}"));
            return null;
        }

        var pedidoId = Guid.NewGuid();
        var orderItems = command.ItensDoPedido
            .Select(i => MapItemDoPedido(i, pedidoId, productsEntityList.First(p => p.Id == i.ProdutoId))).ToList();

        var pedido = new Pedido(pedidoId, command.ClienteId, orderItems);
        var pedidoEntity = await pedidoGateway.CreateAsync(pedido);

        return pedidoEntity;
    }

    private static ItemDoPedido MapItemDoPedido(ItemDoPedidoDto itemDoPedidoDTO, Guid pedidoId, Produto produto)
    {
        return new ItemDoPedido(pedidoId, produto, itemDoPedidoDTO.Quantidade);
    }
}
using Pedidos.Adapters.Presenters.Pedidos;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Domain.Produtos.Enums;

namespace Pedidos.Tests.UnitTests.Adapters.Presenters.UseCases;
public class PedidoPresenterTests
{
    [Fact]
    public void ToPedidoDto_DeveConverterPedidoParaPedidoDtoCorretamente()
    {
        // Arrange
        var produto = new Produto("Produto Teste", "Descrição Teste", 10.0m, "http://imagem.com", ProdutoCategoria.Bebida);
        var itemDoPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        var pedido = new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido> { itemDoPedido });

        // Act
        var pedidoDto = PedidoPresenter.ToPedidoDto(pedido);

        // Assert
        Assert.Equal(pedido.Id, pedidoDto.Id);
        Assert.Equal(pedido.DataPedido, pedidoDto.DataPedido);
        Assert.Equal(pedido.StatusPedido, pedidoDto.StatusPedido);
        Assert.Equal(pedido.ValorTotal, pedidoDto.ValorTotal);
        Assert.Single(pedidoDto.ItensDoPedido);
        Assert.Equal(itemDoPedido.Id, pedidoDto.ItensDoPedido.First().Id);
        Assert.Equal(itemDoPedido.ProdutoId, pedidoDto.ItensDoPedido.First().ProdutoId);
        Assert.Equal(itemDoPedido.Quantidade, pedidoDto.ItensDoPedido.First().Quantidade);
        Assert.Equal(itemDoPedido.Produto.Imagem, pedidoDto.ItensDoPedido.First().Imagem);
    }

    [Fact]
    public void ToListPedidoDto_DeveConverterListaDePedidosParaListaDePedidoDtoCorretamente()
    {
        // Arrange
        var produto = new Produto("Produto Teste", "Descrição Teste", 10.0m, "http://imagem.com", ProdutoCategoria.Bebida);
        var itemDoPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        var pedido = new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido> { itemDoPedido });
        var pedidos = new List<Pedido> { pedido };

        // Act
        var pedidosDto = PedidoPresenter.ToListPedidoDto(pedidos);

        // Assert
        Assert.Single(pedidosDto);
        Assert.Equal(pedido.Id, pedidosDto[0].Id);
        Assert.Equal(pedido.DataPedido, pedidosDto[0].DataPedido);
        Assert.Equal(pedido.StatusPedido, pedidosDto[0].StatusPedido);
        Assert.Equal(pedido.ValorTotal, pedidosDto[0].ValorTotal);
        Assert.Single(pedidosDto[0].ItensDoPedido);
        Assert.Equal(itemDoPedido.Id, pedidosDto[0].ItensDoPedido.First().Id);
        Assert.Equal(itemDoPedido.ProdutoId, pedidosDto[0].ItensDoPedido.First().ProdutoId);
        Assert.Equal(itemDoPedido.Quantidade, pedidosDto[0].ItensDoPedido.First().Quantidade);
        Assert.Equal(itemDoPedido.Produto.Imagem, pedidosDto[0].ItensDoPedido.First().Imagem);
    }
}
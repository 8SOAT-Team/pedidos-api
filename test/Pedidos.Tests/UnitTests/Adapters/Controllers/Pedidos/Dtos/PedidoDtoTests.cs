using Pedidos.Adapters.Controllers.Pedidos.Dtos;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Tests.UnitTests.Adapters.Controllers.Pedidos.Dtos;
public class PedidoDtoTests
{
    [Fact]
    public void CriarPedidoDto_DeveCriarComValoresCorretos()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dataPedido = DateTime.UtcNow;
        var status = StatusPedido.Finalizado;
        var itens = new List<ItemDoPedidoDto> { new ItemDoPedidoDto { Id = Guid.NewGuid(), ProdutoId = Guid.NewGuid(), Quantidade = 1, Imagem = "img.png" } };
        var valorTotal = 100.50m;

        // Act
        var pedido = new PedidoDto { Id = id, DataPedido = dataPedido, StatusPedido = status, ItensDoPedido = itens, ValorTotal = valorTotal };

        // Assert
        Assert.Equal(id, pedido.Id);
        Assert.Equal(dataPedido, pedido.DataPedido);
        Assert.Equal(status, pedido.StatusPedido);
        Assert.Equal(itens, pedido.ItensDoPedido);
        Assert.Equal(valorTotal, pedido.ValorTotal);
    }

    [Fact]
    public void PedidoDto_ItensDoPedidoDeveSerSomenteLeitura()
    {
        // Arrange
        var pedido = new PedidoDto
        {
            ItensDoPedido = new List<ItemDoPedidoDto>()
        };

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => ((List<ItemDoPedidoDto>)pedido.ItensDoPedido).Add(new ItemDoPedidoDto { Id = Guid.NewGuid(), ProdutoId = Guid.NewGuid(), Quantidade = 1, Imagem = "img.png" }));
    }

    [Fact]
    public void PedidoDto_ValorTotalDeveSerPositivo()
    {
        // Arrange
        var pedido = new PedidoDto { Id = Guid.NewGuid(), DataPedido = DateTime.UtcNow, StatusPedido = StatusPedido.EmPreparacao, ValorTotal = 150.75m };

        // Assert
        Assert.True(pedido.ValorTotal >= 0);
    }

    [Fact]
    public void PedidoDto_Igualdade_DeveRetornarTrueParaObjetosIguais()
    {
        // Arrange
        var id = Guid.NewGuid();
        var pedido1 = new PedidoDto { Id = id, DataPedido = DateTime.UtcNow, StatusPedido = StatusPedido.EmPreparacao, ValorTotal = 50 };
        var pedido2 = new PedidoDto { Id = id, DataPedido = DateTime.UtcNow, StatusPedido = StatusPedido.EmPreparacao, ValorTotal = 50 };

        // Act & Assert
        Assert.Equal(pedido1, pedido2);
    }

    [Fact]
    public void PedidoDto_CompararComNull_DeveRetornarFalse()
    {
        // Arrange
        var pedido = new PedidoDto { Id = Guid.NewGuid(), DataPedido = DateTime.UtcNow, StatusPedido = StatusPedido.EmPreparacao, ValorTotal = 50 };

        // Act & Assert
        Assert.False(pedido.Equals(null));
    }
}

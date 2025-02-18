using Pedidos.Adapters.Controllers.Pedidos.Dtos;

namespace Pedidos.Tests.UnitTests.Adapters.Controllers.Pedidos.Dtos;
public class NovoPedidoDtoTests
{
    [Fact]
    public void CriarNovoPedidoDto_DeveCriarComValoresCorretos()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var itens = new List<NovoItemDePedido>
        {
            new NovoItemDePedido { ProdutoId = Guid.NewGuid(), Quantidade = 2 }
        };

        // Act
        var pedido = new NovoPedidoDto { ClienteId = clienteId, ItensDoPedido = itens };

        // Assert
        Assert.Equal(clienteId, pedido.ClienteId);
        Assert.Equal(itens, pedido.ItensDoPedido);
    }

    [Fact]
    public void NovoPedidoDto_ItensDoPedidoNaoPodeSerNulo()
    {
        // Act & Assert
        Assert.Throws<NullReferenceException>(() => new NovoPedidoDto { ClienteId = Guid.NewGuid(), ItensDoPedido = null! });
    }

    [Fact]
    public void NovoPedidoDto_PodeTerClienteNulo()
    {
        // Arrange
        var itens = new List<NovoItemDePedido> { new NovoItemDePedido { ProdutoId = Guid.NewGuid(), Quantidade = 1 } };

        // Act
        var pedido = new NovoPedidoDto { ClienteId = null, ItensDoPedido = itens };

        // Assert
        Assert.Null(pedido.ClienteId);
    }

    [Fact]
    public void NovoPedidoDto_ListaDeItensDeveSerMutavel()
    {
        // Arrange
        var pedido = new NovoPedidoDto { ClienteId = Guid.NewGuid(), ItensDoPedido = new List<NovoItemDePedido>() };

        // Act
        pedido.ItensDoPedido.Add(new NovoItemDePedido { ProdutoId = Guid.NewGuid(), Quantidade = 3 });

        // Assert
        Assert.Single(pedido.ItensDoPedido);
    }

    [Fact]
    public void NovoPedidoDto_Igualdade_DeveRetornarFalseParaObjetosDiferentes()
    {
        // Arrange
        var pedido1 = new NovoPedidoDto { ClienteId = Guid.NewGuid(), ItensDoPedido = new List<NovoItemDePedido>() };
        var pedido2 = new NovoPedidoDto { ClienteId = Guid.NewGuid(), ItensDoPedido = new List<NovoItemDePedido>() };

        // Act & Assert
        Assert.False(pedido1.Equals(pedido2));
    }
}
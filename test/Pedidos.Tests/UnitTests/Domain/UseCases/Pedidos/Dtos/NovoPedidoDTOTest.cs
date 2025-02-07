using Pedidos.Apps.Pedidos.UseCases.Dtos;

namespace Pedidos.Tests.UnitTests.Domain.UseCases.Pedidos.Dtos;
public class NovoPedidoDtoTest
{

    [Fact]
    public void DeveCriarNovoPedidoDto_ComClienteIdValidoEItens()
    {
        // Arrange
        Guid clienteId = Guid.NewGuid();
        var item1 = new ItemDoPedidoDto
        {
            Id = Guid.NewGuid(),
            ProdutoId = Guid.NewGuid(),
            Quantidade = 5
        };
        var item2 = new ItemDoPedidoDto
        {
            Id = Guid.NewGuid(),
            ProdutoId = Guid.NewGuid(),
            Quantidade = 2
        };
        var itens = new List<ItemDoPedidoDto> { item1, item2 };

        // Act
        var novoPedido = new NovoPedidoDto
        {
            ClienteId = clienteId,
            ItensDoPedido = itens
        };

        // Assert
        Assert.Equal(clienteId, novoPedido.ClienteId);
        Assert.Equal(itens.Count, novoPedido.ItensDoPedido.Count);
        Assert.Equal(item1, novoPedido.ItensDoPedido[0]);
        Assert.Equal(item2, novoPedido.ItensDoPedido[1]);
    }

    [Fact]
    public void DeveCriarNovoPedidoDto_ComClienteIdNulo()
    {
        // Arrange
        var item1 = new ItemDoPedidoDto
        {
            Id = Guid.NewGuid(),
            ProdutoId = Guid.NewGuid(),
            Quantidade = 5
        };
        var item2 = new ItemDoPedidoDto
        {
            Id = Guid.NewGuid(),
            ProdutoId = Guid.NewGuid(),
            Quantidade = 3
        };
        var itens = new List<ItemDoPedidoDto> { item1, item2 };

        // Act
        var novoPedido = new NovoPedidoDto
        {
            ClienteId = null,  
            ItensDoPedido = itens
        };

        // Assert
        Assert.Null(novoPedido.ClienteId);
        Assert.Equal(itens.Count, novoPedido.ItensDoPedido.Count);
    }

    [Fact]
    public void DeveCriarNovoPedidoDto_ComItensVazios()
    {
        // Arrange
        var itensVazios = new List<ItemDoPedidoDto>();

        // Act
        var novoPedido = new NovoPedidoDto
        {
            ClienteId = Guid.NewGuid(),
            ItensDoPedido = itensVazios
        };

        // Assert
        Assert.Empty(novoPedido.ItensDoPedido); 
    }

    [Fact]
    public void DeveAdicionarItensNaListaDeItensDoPedido()
    {
        // Arrange
        var item1 = new ItemDoPedidoDto
        {
            Id = Guid.NewGuid(),
            ProdutoId = Guid.NewGuid(),
            Quantidade = 5
        };
        var item2 = new ItemDoPedidoDto
        {
            Id = Guid.NewGuid(),
            ProdutoId = Guid.NewGuid(),
            Quantidade = 3
        };
        var itens = new List<ItemDoPedidoDto> { item1 };

        // Act
        itens.Add(item2);
        var novoPedido = new NovoPedidoDto
        {
            ClienteId = Guid.NewGuid(),
            ItensDoPedido = itens
        };

        // Assert
        Assert.Equal(2, novoPedido.ItensDoPedido.Count); 
        Assert.Equal(item1, novoPedido.ItensDoPedido[0]);
        Assert.Equal(item2, novoPedido.ItensDoPedido[1]);
    }

}

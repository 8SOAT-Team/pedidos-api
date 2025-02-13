using Pedidos.Apps.Pedidos.UseCases.Dtos;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Tests.UnitTests.Application.Pedidos.UseCase.Dtos;
public class ItemDoPedidoDtoTests
{
    [Fact]
    public void Deve_Criar_ItemDoPedidoDto_Corretamente()
    {
        // Arrange
        var id = Guid.NewGuid();
        var produtoId = Guid.NewGuid();
        var quantidade = 5;

        // Act
        var item = new ItemDoPedidoDto
        {
            Id = id,
            ProdutoId = produtoId,
            Quantidade = quantidade
        };

        // Assert
        Assert.Equal(id, item.Id);
        Assert.Equal(produtoId, item.ProdutoId);
        Assert.Equal(quantidade, item.Quantidade);
    }
}

public class NovoPedidoDtoTests
{
    [Fact]
    public void Deve_Criar_NovoPedidoDto_Com_Itens()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var itens = new List<ItemDoPedidoDto>
            {
                new ItemDoPedidoDto { Id = Guid.NewGuid(), ProdutoId = Guid.NewGuid(), Quantidade = 2 },
                new ItemDoPedidoDto { Id = Guid.NewGuid(), ProdutoId = Guid.NewGuid(), Quantidade = 3 }
            };

        // Act
        var pedido = new NovoPedidoDto
        {
            ClienteId = clienteId,
            ItensDoPedido = itens
        };

        // Assert
        Assert.Equal(clienteId, pedido.ClienteId);
        Assert.Equal(itens.Count, pedido.ItensDoPedido.Count);
    }
}

public class NovoStatusDePedidoDtoTests
{
    [Fact]
    public void Deve_Criar_NovoStatusDePedidoDto_Corretamente()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        var novoStatus = StatusPedido.Pronto;

        // Act
        var statusDto = new NovoStatusDePedidoDto
        {
            PedidoId = pedidoId,
            NovoStatus = novoStatus
        };

        // Assert
        Assert.Equal(pedidoId, statusDto.PedidoId);
        Assert.Equal(novoStatus, statusDto.NovoStatus);
    }
}


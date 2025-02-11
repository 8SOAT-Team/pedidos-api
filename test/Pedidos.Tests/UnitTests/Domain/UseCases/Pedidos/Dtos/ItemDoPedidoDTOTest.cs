using Pedidos.Adapters.Controllers.Pedidos.Dtos;

namespace Pedidos.Tests.UnitTests.Domain.UseCases.Pedidos.Dtos;

public class ItemDoPedidoDtoTest
{
    [Fact]
    public void DeveCriarItemDoPedidoDto_ComValoresCorretos()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Guid produtoId = Guid.NewGuid();
        int quantidade = 10;

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


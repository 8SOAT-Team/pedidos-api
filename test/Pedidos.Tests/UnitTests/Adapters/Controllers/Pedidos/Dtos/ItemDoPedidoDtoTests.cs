using Pedidos.Adapters.Controllers.Pedidos.Dtos;

namespace Pedidos.Tests.UnitTests.Adapters.Controllers.Pedidos.Dtos;
public class ItemDoPedidoDtoTests
{
    [Fact]
    public void CriarItemDoPedidoDto_DeveCriarComValoresCorretos()
    {
        // Arrange
        var id = Guid.NewGuid();
        var produtoId = Guid.NewGuid();
        var quantidade = 5;
        var imagem = "produto.jpg";

        // Act
        var item = new ItemDoPedidoDto { Id = id, ProdutoId = produtoId, Quantidade = quantidade, Imagem = imagem };

        // Assert
        Assert.Equal(id, item.Id);
        Assert.Equal(produtoId, item.ProdutoId);
        Assert.Equal(quantidade, item.Quantidade);
        Assert.Equal(imagem, item.Imagem);
    }

    [Fact]
    public void ItemDoPedidoDto_DeveTerImagemNaoNula()
    {
        // Arrange & Act
        var item = new ItemDoPedidoDto { Id = Guid.NewGuid(), ProdutoId = Guid.NewGuid(), Quantidade = 3, Imagem = "imagem.png" };

        // Assert
        Assert.NotNull(item.Imagem);
    }

    [Fact]
    public void ItemDoPedidoDto_QuantidadeDeveSerMaiorQueZero()
    {
        // Arrange
        var item = new ItemDoPedidoDto { Id = Guid.NewGuid(), ProdutoId = Guid.NewGuid(), Quantidade = 1, Imagem = "imagem.png" };

        // Assert
        Assert.True(item.Quantidade > 0);
    }

    [Fact]
    public void ItemDoPedidoDto_Igualdade_DeveRetornarTrueParaObjetosIguais()
    {
        // Arrange
        var id = Guid.NewGuid();
        var produtoId = Guid.NewGuid();
        var item1 = new ItemDoPedidoDto { Id = id, ProdutoId = produtoId, Quantidade = 2, Imagem = "img.png" };
        var item2 = new ItemDoPedidoDto { Id = id, ProdutoId = produtoId, Quantidade = 2, Imagem = "img.png" };

        // Act & Assert
        Assert.Equal(item1, item2);
    }

    [Fact]
    public void ItemDoPedidoDto_CompararComNull_DeveRetornarFalse()
    {
        // Arrange
        var item = new ItemDoPedidoDto { Id = Guid.NewGuid(), ProdutoId = Guid.NewGuid(), Quantidade = 2, Imagem = "img.png" };

        // Act & Assert
        Assert.False(item.Equals(null));
    }
}
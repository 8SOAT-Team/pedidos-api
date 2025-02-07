using Moq;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Domain.Produtos.Enums;
using AppProdutoCategoria = Pedidos.Apps.Produtos.Enums.ProdutoCategoria;

namespace Pedidos.Tests.UnitTests.Domain.UseCases.Abstractions.Gateways;

public class ProdutoGatewayTest
{
    [Fact]
    public async Task CreateProdutoAsync_DeveCriarProduto()
    {
        // Arrange
        var mockGateway = new Mock<IProdutoGateway>();
        var produto = new Produto(Guid.NewGuid(), "Lanche", "Lanche de bacon", 50m, "http://exemplo.com/imagem.jpg",
            ProdutoCategoria.Acompanhamento);

        mockGateway.Setup(gateway => gateway.CreateProdutoAsync(produto)).ReturnsAsync(produto);

        // Act
        var resultado = await mockGateway.Object.CreateProdutoAsync(produto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(produto.Id, resultado.Id);
        Assert.Equal(produto.Nome, resultado.Nome);
        Assert.Equal(produto.Preco, resultado.Preco);
    }

    [Fact]
    public async Task GetProdutoByIdAsync_DeveRetornarProdutoQuandoIdValido()
    {
        // Arrange
        var mockGateway = new Mock<IProdutoGateway>();
        var produtoId = Guid.NewGuid();
        var produto = new Produto(produtoId, "Lanche", "Lanche de bacon", 50m, "http://exemplo.com/imagem.jpg",
            ProdutoCategoria.Acompanhamento);

        mockGateway.Setup(gateway => gateway.GetProdutoByIdAsync(produtoId)).ReturnsAsync(produto);

        // Act
        var resultado = await mockGateway.Object.GetProdutoByIdAsync(produtoId);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(produtoId, resultado.Id);
    }

    [Fact]
    public async Task GetProdutoCompletoByIdAsync_DeveRetornarProdutoCompletoQuandoIdValido()
    {
        // Arrange
        var mockGateway = new Mock<IProdutoGateway>();
        var produtoId = Guid.NewGuid();
        var produto = new Produto(produtoId, "Lanche", "Lanche de bacon", 50m, "http://exemplo.com/imagem.jpg",
            ProdutoCategoria.Acompanhamento);

        mockGateway.Setup(gateway => gateway.GetProdutoCompletoByIdAsync(produtoId)).ReturnsAsync(produto);

        // Act
        var resultado = await mockGateway.Object.GetProdutoCompletoByIdAsync(produtoId);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(produtoId, resultado.Id);
    }


    [Fact]
    public async Task GetProdutosByCategoriaAsync_DeveRetornarProdutosDaCategoria()
    {
        // Arrange
        var mockGateway = new Mock<IProdutoGateway>();
        var categoria = AppProdutoCategoria.Acompanhamento;
        var produtos = new List<Produto>
        {
            new Produto(Guid.NewGuid(), "Lanche", "Lanche de bacon", 50m, "http://exemplo.com/imagem1.jpg",
                ProdutoCategoria.Acompanhamento),
            new Produto(Guid.NewGuid(), "Suco", "Suco de laranja", 10m, "http://exemplo.com/imagem2.jpg",
                ProdutoCategoria.Acompanhamento)
        };

        mockGateway.Setup(gateway => gateway.GetProdutosByCategoriaAsync(categoria)).ReturnsAsync(produtos);

        // Act
        var resultado = await mockGateway.Object.GetProdutosByCategoriaAsync(categoria);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
    }

    [Fact]
    public async Task ListarTodosProdutosAsync_DeveRetornarListaDeProdutos()
    {
        // Arrange
        var mockGateway = new Mock<IProdutoGateway>();
        var produtos = new List<Produto>
        {
            new Produto(Guid.NewGuid(), "Lanche", "Lanche de bacon", 50m, "http://exemplo.com/imagem1.jpg",
                ProdutoCategoria.Acompanhamento),
            new Produto(Guid.NewGuid(), "Suco", "Suco de laranja", 10m, "http://exemplo.com/imagem2.jpg",
                ProdutoCategoria.Acompanhamento)
        };

        mockGateway.Setup(gateway => gateway.ListarTodosProdutosAsync()).ReturnsAsync(produtos);

        // Act
        var resultado = await mockGateway.Object.ListarTodosProdutosAsync();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
    }

    [Fact]
    public async Task ListarProdutosByIdAsync_DeveRetornarProdutosQuandoIdsValidos()
    {
        // Arrange
        var mockGateway = new Mock<IProdutoGateway>();
        var produtoIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var produtos = new List<Produto>
        {
            new Produto(produtoIds[0], "Lanche", "Lanche de bacon", 50m, "http://exemplo.com/imagem1.jpg",
                ProdutoCategoria.Acompanhamento),
            new Produto(produtoIds[1], "Suco", "Suco de laranja", 10m, "http://exemplo.com/imagem2.jpg",
                ProdutoCategoria.Acompanhamento)
        };

        mockGateway.Setup(gateway => gateway.ListarProdutosByIdAsync(It.IsAny<ICollection<Guid>>()))
            .ReturnsAsync(produtos);

        // Act
        var resultado = await mockGateway.Object.ListarProdutosByIdAsync(produtoIds);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
    }
}
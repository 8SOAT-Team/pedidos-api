using CleanArch.UseCase.Faults;
using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Produtos.Enums;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Apps.Produtos.UseCases;
using Pedidos.Domain.Produtos.Entities;
using DomainProdutoCategoria = Pedidos.Domain.Produtos.Enums.ProdutoCategoria;

namespace Pedidos.Tests.UnitTests.Domain.UseCases.Produtos;
public class ListarProdutosPorCategoriaUseCaseTest
{
    private readonly Mock<ILogger<ListarProdutoPorCategoriaUseCase>> _loggerMock;
    private readonly Mock<IProdutoGateway> _produtoGatewayMock;
    private readonly ListarProdutoPorCategoriaTest _useCase;

    public ListarProdutosPorCategoriaUseCaseTest()
    {
        _loggerMock = new Mock<ILogger<ListarProdutoPorCategoriaUseCase>>();
        _produtoGatewayMock = new Mock<IProdutoGateway>();
        _useCase = new ListarProdutoPorCategoriaTest(_loggerMock.Object, _produtoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_ProdutosExistemParaCategoria_RetornaListaDeProdutos()
    {
        // Arrange
        var categoriaId = ProdutoCategoria.Acompanhamento;
        var produto1 = new Produto("Lanche1", "Lanche de bacon2", 100.00m, "http://endereco/imagens/img.jpg", DomainProdutoCategoria.Acompanhamento);
        var produto2 = new Produto("Lanche2", "Lanche de bacon2", 150.00m, "http://endereco/imagens/img.jpg", DomainProdutoCategoria.Acompanhamento);

        var produtos = new List<Produto> { produto1, produto2 };

        _produtoGatewayMock.Setup(pg => pg.GetProdutosByCategoriaAsync(It.IsAny<ProdutoCategoria>())).ReturnsAsync(produtos);

        // Act
        var result = await _useCase.Execute(categoriaId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _produtoGatewayMock.Verify(pg => pg.GetProdutosByCategoriaAsync(It.IsAny<ProdutoCategoria>()), Times.Once);
    }

    [Fact]
    public async Task Execute_NaoExistemProdutosParaCategoria_RetornaListaVazia()
    {
        // Arrange
        var categoriaId = ProdutoCategoria.Acompanhamento;
        var produtos = new List<Produto>();

        _produtoGatewayMock.Setup(pg => pg.GetProdutosByCategoriaAsync(It.IsAny<ProdutoCategoria>())).ReturnsAsync(produtos);

        // Act
        var result = await _useCase.Execute(categoriaId);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _produtoGatewayMock.Verify(pg => pg.GetProdutosByCategoriaAsync(It.IsAny<ProdutoCategoria>()), Times.Once);
    }

    [Fact]
    public async Task Execute_ErroNoGateway_ExecutaLogger()
    {
        // Arrange
        var categoriaId = ProdutoCategoria.Acompanhamento;
        _produtoGatewayMock.Setup(pg => pg.GetProdutosByCategoriaAsync(It.IsAny<ProdutoCategoria>())).ThrowsAsync(new Exception("Erro ao acessar o repositório"));

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _useCase.Execute(categoriaId));

        // Assert
        Assert.Equal("Erro ao acessar o repositório", exception.Message);
    }
}

public class ListarProdutoPorCategoriaTest : ListarProdutoPorCategoriaUseCase
{
    public ListarProdutoPorCategoriaTest(ILogger<ListarProdutoPorCategoriaUseCase> logger, IProdutoGateway produtoGateway)
        : base(logger, produtoGateway) { }

    public new Task<ICollection<Produto>?> Execute(ProdutoCategoria command)
    {
        return base.Execute(command);
    }

}
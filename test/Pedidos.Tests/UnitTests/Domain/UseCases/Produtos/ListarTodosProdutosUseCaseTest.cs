﻿using CleanArch.UseCase.Options;
using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Apps.Produtos.UseCases;
using Pedidos.Domain.Produtos.Entities;
using DomainProdutoCategoria = Pedidos.Domain.Produtos.Enums.ProdutoCategoria;

namespace Pedidos.Tests.UnitTests.Domain.UseCases.Produtos;
public class ListarTodosProdutosUseCaseTest
{

    private readonly Mock<ILogger<ListarTodosProdutosUseCase>> _loggerMock;
    private readonly Mock<IProdutoGateway> _produtoGatewayMock;
    private readonly ListarTodosProdutosTest _useCase;

    public ListarTodosProdutosUseCaseTest()
    {
        _loggerMock = new Mock<ILogger<ListarTodosProdutosUseCase>>();
        _produtoGatewayMock = new Mock<IProdutoGateway>();
        _useCase = new ListarTodosProdutosTest(_loggerMock.Object, _produtoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_ProdutosExistem_RetornaListaDeProdutos()
    {
        // Arrange
        var produto1 = new Produto("Lanche1", "Lanche de bacon2", 100.00m, "http://endereco/imagens/img.jpg",
            DomainProdutoCategoria.Acompanhamento);
        var produto2 = new Produto("Lanche2", "Lanche de bacon2", 150.00m, "http://endereco/imagens/img.jpg", DomainProdutoCategoria.Acompanhamento);

        var produtos = new List<Produto> { produto1, produto2 };

        _produtoGatewayMock.Setup(pg => pg.ListarTodosProdutosAsync()).ReturnsAsync(produtos);

        // Act
        var result = await _useCase.Execute(null);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _produtoGatewayMock.Verify(pg => pg.ListarTodosProdutosAsync(), Times.Once);
    }

    [Fact]
    public async Task Execute_NaoExistemProdutos_RetornaListaVazia()
    {
        // Arrange
        var produtos = new List<Produto>();
        _produtoGatewayMock.Setup(pg => pg.ListarTodosProdutosAsync()).ReturnsAsync(produtos);

        // Act
        var result = await _useCase.Execute(null);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _produtoGatewayMock.Verify(pg => pg.ListarTodosProdutosAsync(), Times.Once);
    }

    [Fact]
    public async Task Execute_ErroNoGateway_ExecutaLogger()
    {
        // Arrange
        _produtoGatewayMock.Setup(pg => pg.ListarTodosProdutosAsync()).ThrowsAsync(new Exception("Erro ao acessar o repositório"));

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _useCase.Execute(null));

        // Assert
        Assert.Equal("Erro ao acessar o repositório", exception.Message);
    }
}

public class ListarTodosProdutosTest : ListarTodosProdutosUseCase
{
    public ListarTodosProdutosTest(ILogger<ListarTodosProdutosUseCase> logger, IProdutoGateway produtoGateway)
        : base(logger, produtoGateway) { }

    public new Task<ICollection<Produto>?> Execute(Empty<object> empty)
    {
        return base.Execute(empty);
    }

}

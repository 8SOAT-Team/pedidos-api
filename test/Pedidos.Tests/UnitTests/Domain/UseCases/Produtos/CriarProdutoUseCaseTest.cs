using Bogus;
using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Produtos.Enums;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Apps.Produtos.UseCases;
using Pedidos.Apps.Produtos.UseCases.DTOs;
using Pedidos.Domain.Produtos.Entities;
using DomainProdutoCategoria = Pedidos.Domain.Produtos.Enums.ProdutoCategoria;

namespace Pedidos.Tests.UnitTests.Domain.UseCases.Produtos;

public class CriarProdutoUseCaseTest
{
    private readonly Mock<ILogger<CriarProdutoUseCase>> _loggerMock;
    private readonly Mock<IProdutoGateway> _produtoGatewayMock;
    private readonly CriarProdutoTest _useCase;
    private readonly Faker _faker = new();
    
    public CriarProdutoUseCaseTest()
    {
        _loggerMock = new Mock<ILogger<CriarProdutoUseCase>>();
        _produtoGatewayMock = new Mock<IProdutoGateway>();
        _useCase = new CriarProdutoTest(_loggerMock.Object, _produtoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_ProdutoCriadoComSucesso_RetornaProdutoCriado()
    {
        // Arrange
        var NovoProdutoDto = new NovoProdutoDto
        {
            Nome = "Produto Teste",
            Descricao = "Descrição do produto",
            Preco = 10.0m,
            Imagem = "http://endereco/imagens/img.jpg",
            Categoria = ProdutoCategoria.Acompanhamento
        };

        var produtosNaCategoria = new List<Produto>();

        var produtoCriado = new Produto(Guid.NewGuid(), "Produto Teste", "Descrição do produto", 10.0m,
            "http://endereco/imagens/img.jpg", DomainProdutoCategoria.Acompanhamento);

        _produtoGatewayMock.Setup(pg => pg.GetProdutosByCategoriaAsync(It.IsAny<ProdutoCategoria>())).ReturnsAsync(produtosNaCategoria);
        _produtoGatewayMock.Setup(pg => pg.CreateProdutoAsync(It.IsAny<Produto>())).ReturnsAsync(produtoCriado);

        // Act
        var result = await _useCase.Execute(NovoProdutoDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(NovoProdutoDto.Nome, result.Nome);
        _produtoGatewayMock.Verify(pg => pg.CreateProdutoAsync(It.IsAny<Produto>()), Times.Once);
    }

    [Fact]
    public async Task Execute_ErroAoCriarProduto_RetornaNull()
    {
        // Arrange
        var NovoProdutoDto = new NovoProdutoDto
        {
            Nome = "Produto Teste",
            Descricao = "Descrição do produto",
            Preco = 10.0m,
            Imagem = "http://endereco/imagens/img.jpg",
            Categoria = ProdutoCategoria.Acompanhamento
        };

        var produtosNaCategoria = new List<Produto>();

        _produtoGatewayMock.Setup(pg => pg.GetProdutosByCategoriaAsync(It.IsAny<ProdutoCategoria>())).ReturnsAsync(produtosNaCategoria);
        _produtoGatewayMock.Setup(pg => pg.CreateProdutoAsync(It.IsAny<Produto>()))
            .ThrowsAsync(new Exception("Erro ao salvar produto"));

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _useCase.Execute(NovoProdutoDto));

        // Assert
        Assert.Equal("Erro ao salvar produto", exception.Message);
    }
}

public class CriarProdutoTest : CriarProdutoUseCase
{
    public CriarProdutoTest(ILogger<CriarProdutoUseCase> logger, IProdutoGateway produtoGateway)
        : base(logger, produtoGateway)
    {
    }

    public new Task<Produto?> Execute(NovoProdutoDto command)
    {
        return base.Execute(command);
    }
}
using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Pedidos.UseCases;
using Pedidos.Apps.Pedidos.UseCases.Dtos;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Domain.Produtos.Enums;

namespace Pedidos.Tests.UnitTests.Application.Pedidos.UseCase;
public class CriarNovoPedidoUseCaseTests
{
    private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
    private readonly Mock<IProdutoGateway> _produtoGatewayMock;
    private readonly CriarNovoPedidoUseCase _useCase;

    public CriarNovoPedidoUseCaseTests()
    {
        _pedidoGatewayMock = new Mock<IPedidoGateway>();
        _produtoGatewayMock = new Mock<IProdutoGateway>();
        _useCase = new CriarNovoPedidoUseCase(
            new Mock<ILogger<CriarNovoPedidoUseCase>>().Object,
            _pedidoGatewayMock.Object,
            _produtoGatewayMock.Object
        );
    }

    [Fact]
    public async Task Execute_ShouldCreateOrder_WhenProductsExist()
    {
        // Arrange
        var produto = new Produto(Guid.NewGuid(), "Produto Teste", "Descrição do Produto", 100, "Imagem do Produto",ProdutoCategoria.Bebida);
        var pedidoDto = new NovoPedidoDto
        {
            ClienteId = Guid.NewGuid(),
            ItensDoPedido = new List<ItemDoPedidoDto>
                {
                    new ItemDoPedidoDto { ProdutoId = produto.Id, Quantidade = 2 }
                }
        };

        _produtoGatewayMock.Setup(g => g.ListarProdutosByIdAsync(It.IsAny<Guid[]>())).ReturnsAsync(new List<Produto> { produto });
        _pedidoGatewayMock.Setup(g => g.CreateAsync(It.IsAny<Pedido>())).ReturnsAsync(new Pedido(Guid.NewGuid(), pedidoDto.ClienteId, new List<ItemDoPedido>()));

        // Act
        var result = await _useCase.ResolveAsync(pedidoDto);

        // Assert
        Assert.NotNull(result);
        _pedidoGatewayMock.Verify(g => g.CreateAsync(It.IsAny<Pedido>()), Times.Once);
    }

    [Fact]
    public async Task Execute_ShouldReturnError_WhenProductNotFound()
    {
        // Arrange
        var pedidoDto = new NovoPedidoDto
        {
            ClienteId = Guid.NewGuid(),
            ItensDoPedido = new List<ItemDoPedidoDto>
                {
                    new ItemDoPedidoDto { ProdutoId = Guid.NewGuid(), Quantidade = 2 }
                }
        };

        _produtoGatewayMock.Setup(g => g.ListarProdutosByIdAsync(It.IsAny<Guid[]>())).ReturnsAsync(new List<Produto>());

        // Act
        var result = await _useCase.ResolveAsync(pedidoDto);

        // Assert
        Assert.Null(result);
        Assert.Single(_useCase.GetErrors());
        Assert.Equal("Produto não encontrado: " + pedidoDto.ItensDoPedido[0].ProdutoId, _useCase.GetErrors().First().Description);
    }

    [Fact]
    public async Task Execute_ShouldCreateOrder_WhenMultipleItemsExist()
    {
        // Arrange
        var produto1 = new Produto(Guid.NewGuid(), "Produto Teste 1", "Descrição do Produto 1", 100, "Imagem do Produto 1", ProdutoCategoria.Bebida);
        var produto2 = new Produto(Guid.NewGuid(), "Produto Teste 2", "Descrição do Produto 2", 200, "Imagem do Produto 2", ProdutoCategoria.Lanche);

        var pedidoDto = new NovoPedidoDto
        {
            ClienteId = Guid.NewGuid(),
            ItensDoPedido = new List<ItemDoPedidoDto>
                {
                    new ItemDoPedidoDto { ProdutoId = produto1.Id, Quantidade = 1 },
                    new ItemDoPedidoDto { ProdutoId = produto2.Id, Quantidade = 3 }
                }
        };

        _produtoGatewayMock.Setup(g => g.ListarProdutosByIdAsync(It.IsAny<Guid[]>())).ReturnsAsync(new List<Produto> { produto1, produto2 });
        _pedidoGatewayMock.Setup(g => g.CreateAsync(It.IsAny<Pedido>())).ReturnsAsync(new Pedido(Guid.NewGuid(), pedidoDto.ClienteId, new List<ItemDoPedido>()));

        // Act
        var result = await _useCase.ResolveAsync(pedidoDto);

        // Assert
        Assert.NotNull(result);
        _pedidoGatewayMock.Verify(g => g.CreateAsync(It.IsAny<Pedido>()), Times.Once);
    }
}
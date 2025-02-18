using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Pedidos.UseCases.Dtos;
using Pedidos.Apps.Pedidos.UseCases;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Pedidos.Enums;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Domain.Produtos.Enums;
using static Moq.It;

namespace Pedidos.Tests.UnitTests.Application.Pedidos.UseCase;
public class AtualizarStatusDePreparoPedidoUseCaseTests
{
    private readonly Mock<ILogger<AtualizarStatusDePreparoPedidoUseCase>> _mockLogger;
    private readonly Mock<IPedidoGateway> _mockPedidoGateway;
    private readonly AtualizarStatusDePreparoPedidoUseCase _useCase;

    public AtualizarStatusDePreparoPedidoUseCaseTests()
    {
        _mockLogger = new Mock<ILogger<AtualizarStatusDePreparoPedidoUseCase>>();
        _mockPedidoGateway = new Mock<IPedidoGateway>();
        _useCase = new AtualizarStatusDePreparoPedidoUseCase(_mockLogger.Object, _mockPedidoGateway.Object);
    }

    [Fact]
    public async Task Execute_ShouldReturnNull_WhenPedidoNotFound()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        _mockPedidoGateway.Setup(x => x.GetByIdAsync(pedidoId)).ReturnsAsync((Pedido?)null);

        var request = new NovoStatusDePedidoDto
        {
            PedidoId = pedidoId,
            NovoStatus = StatusPedido.Pronto
        };

        // Act
        var result = await _useCase.ResolveAsync(request);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Execute_ShouldReturnNull_WhenStatusIsInvalid()
    {
        // Arrange
        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", ProdutoCategoria.Acompanhamento);
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        var pedido = new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido> {itemPedido});
        _mockPedidoGateway.Setup(x => x.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);

        var request = new NovoStatusDePedidoDto
        {
            PedidoId = pedido.Id,
            NovoStatus = (StatusPedido)999 // Invalid status
        };

        // Act
        var result = await _useCase.ResolveAsync(request);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Execute_ShouldReturnUpdatedPedido_WhenStatusIsPronto()
    {
        // Arrange
        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", ProdutoCategoria.Acompanhamento);
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        var pedido = new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>{itemPedido});
        _mockPedidoGateway.Setup(x => x.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);
        _mockPedidoGateway.Setup(x => x.UpdateAsync(pedido)).ReturnsAsync(pedido);

        var request = new NovoStatusDePedidoDto
        {
            PedidoId = pedido.Id,
            NovoStatus = StatusPedido.Pronto
        };

        // Act
        var result = await _useCase.ResolveAsync(request);

        // Assert
        Assert.NotNull(result);        
    }

    [Fact]
    public async Task Execute_ShouldReturnUpdatedPedido_WhenStatusIsFinalizado()
    {
        // Arrange
        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", ProdutoCategoria.Acompanhamento);
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        var pedido = new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>{itemPedido});
        _mockPedidoGateway.Setup(x => x.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);
        _mockPedidoGateway.Setup(x => x.UpdateAsync(pedido)).ReturnsAsync(pedido);

        var request = new NovoStatusDePedidoDto
        {
            PedidoId = pedido.Id,
            NovoStatus = StatusPedido.Finalizado
        };

        // Act
        var result = await _useCase.ResolveAsync(request);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Execute_ShouldReturnUpdatedPedido_WhenStatusIsCancelado()
    {
        // Arrange
        var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", ProdutoCategoria.Acompanhamento);
        var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
        var pedido = new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>{itemPedido});
        _mockPedidoGateway.Setup(x => x.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);
        _mockPedidoGateway.Setup(x => x.UpdateAsync(pedido)).ReturnsAsync(pedido);

        var request = new NovoStatusDePedidoDto
        {
            PedidoId = pedido.Id,
            NovoStatus = StatusPedido.Cancelado
        };

        // Act
        var result = await _useCase.ResolveAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusPedido.Cancelado, result.Value.StatusPedido);
    }
}

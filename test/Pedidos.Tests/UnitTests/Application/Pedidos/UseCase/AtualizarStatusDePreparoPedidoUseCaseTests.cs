using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Pedidos.UseCases.Dtos;
using Pedidos.Apps.Pedidos.UseCases;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Pedidos.Enums;

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
        Assert.Null(result);
        _mockLogger.Verify(log => log.LogError(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Execute_ShouldReturnNull_WhenStatusIsInvalid()
    {
        // Arrange
        var pedido = new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>());
        _mockPedidoGateway.Setup(x => x.GetByIdAsync(pedido.Id)).ReturnsAsync(pedido);

        var request = new NovoStatusDePedidoDto
        {
            PedidoId = pedido.Id,
            NovoStatus = (StatusPedido)999 // Invalid status
        };

        // Act
        var result = await _useCase.ResolveAsync(request);

        // Assert
        Assert.Null(result);
        _mockLogger.Verify(log => log.LogError(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Execute_ShouldReturnUpdatedPedido_WhenStatusIsPronto()
    {
        // Arrange
        var pedido = new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>());
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
        var pedido = new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>());
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
        Assert.Equal(StatusPedido.Finalizado, result.Value.StatusPedido);
    }

    [Fact]
    public async Task Execute_ShouldReturnUpdatedPedido_WhenStatusIsCancelado()
    {
        // Arrange
        var pedido = new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>());
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

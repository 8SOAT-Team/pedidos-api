using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Pedidos.UseCases;
using Pedidos.Domain.Pedidos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Tests.UnitTests.Application.Pedidos.UseCase;
public class EncontrarPedidoPorIdUseCaseTests
{
    private readonly Mock<ILogger<EncontrarPedidoPorIdUseCase>> _loggerMock;
    private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
    private readonly EncontrarPedidoPorIdUseCase _useCase;

    public EncontrarPedidoPorIdUseCaseTests()
    {
        _loggerMock = new Mock<ILogger<EncontrarPedidoPorIdUseCase>>();
        _pedidoGatewayMock = new Mock<IPedidoGateway>();
        _useCase = new EncontrarPedidoPorIdUseCase(_loggerMock.Object, _pedidoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_PedidoExistente_RetornaPedido()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        var expectedPedido = new Pedido(pedidoId, Guid.NewGuid(), new System.Collections.Generic.List<ItemDoPedido>());
        _pedidoGatewayMock.Setup(g => g.GetByIdAsync(pedidoId)).ReturnsAsync(expectedPedido);

        // Act
        var result = await _useCase.ResolveAsync(pedidoId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedPedido.Id, result?.Value.Id);
    }

    [Fact]
    public async Task Execute_PedidoNaoExistente_RetornaNull()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        _pedidoGatewayMock.Setup(g => g.GetByIdAsync(pedidoId)).ReturnsAsync((Pedido?)null);

        // Act
        var result = await _useCase.ResolveAsync(pedidoId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Execute_ChamaGatewayOnce()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        _pedidoGatewayMock.Setup(g => g.GetByIdAsync(pedidoId)).ReturnsAsync(new Pedido(pedidoId, Guid.NewGuid(), new System.Collections.Generic.List<ItemDoPedido>()));

        // Act
        await _useCase.ResolveAsync(pedidoId);

        // Assert
        _pedidoGatewayMock.Verify(g => g.GetByIdAsync(pedidoId), Times.Once);
    }
}
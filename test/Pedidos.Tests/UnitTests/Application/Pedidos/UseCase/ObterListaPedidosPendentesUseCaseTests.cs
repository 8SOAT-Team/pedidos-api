using CleanArch.UseCase.Options;
using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Pedidos.UseCases;
using Pedidos.Domain.Pedidos.Entities;

namespace Pedidos.Tests.UnitTests.Application.Pedidos.UseCase;
public class ObterListaPedidosPendentesUseCaseTests
{
    private readonly Mock<ILogger<ObterListaPedidosPendentesUseCase>> _loggerMock;
    private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
    private readonly ObterListaPedidosPendentesUseCase _useCase;

    public ObterListaPedidosPendentesUseCaseTests()
    {
        _loggerMock = new Mock<ILogger<ObterListaPedidosPendentesUseCase>>();
        _pedidoGatewayMock = new Mock<IPedidoGateway>();
        _useCase = new ObterListaPedidosPendentesUseCase(_loggerMock.Object, _pedidoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_RetornaListaDePedidosPendentes()
    {
        // Arrange
        var expectedPedidosPendentes = new List<Pedido>
            {
                new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>()),
                new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>())
            };

        _pedidoGatewayMock.Setup(g => g.GetAllPedidosPending()).ReturnsAsync(expectedPedidosPendentes);

        // Act
        var result = await _useCase.ResolveAsync(Any<object>.Empty);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedPedidosPendentes.Count, result?.Value.Count);
    }

    [Fact]
    public async Task Execute_RetornaListaVaziaQuandoNaoExistemPedidosPendentes()
    {
        // Arrange
        _pedidoGatewayMock.Setup(g => g.GetAllPedidosPending()).ReturnsAsync(new List<Pedido>());

        // Act
        var result = await _useCase.ResolveAsync(Any<object>.Empty);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Value);
    }

    [Fact]
    public async Task Execute_ChamaGatewayUmaVez()
    {
        // Arrange
        _pedidoGatewayMock.Setup(g => g.GetAllPedidosPending()).ReturnsAsync(new List<Pedido>());

        // Act
        await _useCase.ResolveAsync(Any<object>.Empty);

        // Assert
        _pedidoGatewayMock.Verify(g => g.GetAllPedidosPending(), Times.Once);
    }
}
﻿using CleanArch.UseCase.Options;
using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Pedidos.UseCases;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Domain.Produtos.Enums;

namespace Pedidos.Tests.UnitTests.Application.Pedidos.UseCase;
public class ListarTodosPedidosUseCaseTests
{
    private readonly Mock<ILogger<ListarTodosPedidosUseCase>> _loggerMock;
    private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
    private readonly ListarTodosPedidosUseCase _useCase;

    public ListarTodosPedidosUseCaseTests()
    {
        _loggerMock = new Mock<ILogger<ListarTodosPedidosUseCase>>();
        _pedidoGatewayMock = new Mock<IPedidoGateway>();
        _useCase = new ListarTodosPedidosUseCase(_loggerMock.Object, _pedidoGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_RetornaListaDePedidos()
    {
        // Arrange
        var itemDoPedido = new ItemDoPedido(Guid.NewGuid(), new Produto(Guid.NewGuid(), "Produto Teste", "Descrição do Produto", 100, "https://postech.fiap.com.br", ProdutoCategoria.Bebida), 2);
        var expectedPedidos = new List<Pedido>
            {
                new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>(){itemDoPedido}),
                new Pedido(Guid.NewGuid(), Guid.NewGuid(), new List<ItemDoPedido>(){ itemDoPedido })
            };

        _pedidoGatewayMock.Setup(g => g.GetAllAsync()).ReturnsAsync(expectedPedidos);

        // Act
        var result = await _useCase.ResolveAsync(Any<object>.Empty);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedPedidos.Count, result?.Value.Count);
    }

    [Fact]
    public async Task Execute_RetornaListaVaziaQuandoNaoExistemPedidos()
    {
        // Arrange
        _pedidoGatewayMock.Setup(g => g.GetAllAsync()).ReturnsAsync(new List<Pedido>());

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
        _pedidoGatewayMock.Setup(g => g.GetAllAsync()).ReturnsAsync(new List<Pedido>());

        // Act
        await _useCase.ResolveAsync(Any<object>.Empty);

        // Assert
        _pedidoGatewayMock.Verify(g => g.GetAllAsync(), Times.Once);
    }
}
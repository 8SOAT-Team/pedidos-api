using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Adapters.Controllers.Pedidos;
using Pedidos.Adapters.Controllers.Pedidos.Dtos;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Tests.UnitTests.Adapters.Controllers.Pedidos;
public class PedidoControllerTests
{
    private readonly Mock<IPedidoGateway> _pedidoGatewayMock;
    private readonly Mock<IProdutoGateway> _produtoGatewayMock;
    private readonly PedidoController _controller;
    public PedidoControllerTests()
    {
        var loggerFactory = new LoggerFactory();
        _pedidoGatewayMock = new Mock<IPedidoGateway>();
        _produtoGatewayMock = new Mock<IProdutoGateway>();
        _controller = new PedidoController(loggerFactory, _pedidoGatewayMock.Object, _produtoGatewayMock.Object);
    }
    [Fact]
    public async Task AtualizarStatusDePreparacaoDoPedido_DeveRetornarOk()
    {
        // Arrange
        var pedidoId = Guid.NewGuid();
        var novoStatus = StatusPedido.EmPreparacao;
        var pedido = (Pedido)Activator.CreateInstance(typeof(Pedido), true);
        _pedidoGatewayMock.Setup(x => x.GetByIdAsync(pedidoId)).ReturnsAsync(pedido);
        _pedidoGatewayMock.Setup(x => x.UpdateAsync(It.IsAny<Pedido>())).ReturnsAsync(pedido);

        // Act
        var result = await _controller.AtualizarStatusDePreparacaoDoPedido(novoStatus, pedidoId);
        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public async Task CreatePedidoAsync_DeveRetornarOk()
    {
        // Arrange
        var pedido = new NovoPedidoDto
        {
            ClienteId = Guid.NewGuid(),
            ItensDoPedido = new List<NovoItemDePedido>
            {
                new NovoItemDePedido
                {
                    ProdutoId = Guid.NewGuid(),
                    Quantidade = 1
                }
            }
        };
        var pedidoCriado = (Pedido)Activator.CreateInstance(typeof(Pedido), true);
        _pedidoGatewayMock.Setup(x => x.CreateAsync(It.IsAny<Pedido>())).ReturnsAsync(pedidoCriado);
        // Act
        var result = await _controller.CreatePedidoAsync(pedido);
        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}

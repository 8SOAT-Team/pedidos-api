using Pedidos.Apps.Pedidos.UseCases.Dtos;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Tests.UnitTests.Domain.UseCases.Pedidos.Dtos;
public class NovoStatusDePedidoDtoTest
{

    [Fact]
    public void DeveCriarNovoStatusDePedidoDto_ComValoresCorretos()
    {
        // Arrange
        Guid pedidoId = Guid.NewGuid();
        StatusPedido novoStatus = StatusPedido.Recebido;

        // Act
        var novoStatusDePedido = new NovoStatusDePedidoDto
        {
            PedidoId = pedidoId,
            NovoStatus = novoStatus
        };

        // Assert
        Assert.Equal(pedidoId, novoStatusDePedido.PedidoId);
        Assert.Equal(novoStatus, novoStatusDePedido.NovoStatus);
    }

    [Fact]
    public void DeveCriarNovoStatusDePedidoDto_ComStatusCancelado()
    {
        // Arrange
        Guid pedidoId = Guid.NewGuid();
        StatusPedido novoStatus = StatusPedido.Cancelado;

        // Act
        var novoStatusDePedido = new NovoStatusDePedidoDto
        {
            PedidoId = pedidoId,
            NovoStatus = novoStatus
        };

        // Assert
        Assert.Equal(pedidoId, novoStatusDePedido.PedidoId);
        Assert.Equal(novoStatus, novoStatusDePedido.NovoStatus);
    }

    [Fact]
    public void DeveCriarNovoStatusDePedidoDto_ComStatusConcluido()
    {
        // Arrange
        Guid pedidoId = Guid.NewGuid();
        StatusPedido novoStatus = StatusPedido.Finalizado;

        // Act
        var novoStatusDePedido = new NovoStatusDePedidoDto
        {
            PedidoId = pedidoId,
            NovoStatus = novoStatus
        };

        // Assert
        Assert.Equal(pedidoId, novoStatusDePedido.PedidoId);
        Assert.Equal(novoStatus, novoStatusDePedido.NovoStatus);
    }

    [Fact]
    public void DeveCriarNovoStatusDePedidoDto_ComStatusEmProcessamento()
    {
        // Arrange
        Guid pedidoId = Guid.NewGuid();
        StatusPedido novoStatus = StatusPedido.EmPreparacao;

        // Act
        var novoStatusDePedido = new NovoStatusDePedidoDto
        {
            PedidoId = pedidoId,
            NovoStatus = novoStatus
        };

        // Assert
        Assert.Equal(pedidoId, novoStatusDePedido.PedidoId);
        Assert.Equal(novoStatus, novoStatusDePedido.NovoStatus);
    }

}

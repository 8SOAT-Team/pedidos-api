using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Clientes.Gateways;
using Pedidos.Apps.Clientes.UseCases;
using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Tests.UnitTests.Domain.UseCases.Clientes;
public class IdentificarClienteUseCaseTest
{

    private readonly Mock<ILogger<IdentificarClienteUseCase>> _loggerMock;
    private readonly Mock<IClienteGateway> _clienteGatewayMock;

    public IdentificarClienteUseCaseTest()
    {
        _loggerMock =new Mock<ILogger<IdentificarClienteUseCase>>();
        _clienteGatewayMock = new Mock<IClienteGateway>(); 
    }
    
    [Fact]
    public async Task Execute_DeveRetornarClienteQuandoCpfExistente()
    {
        // Arrange
        var cpf = new Cpf("12345678901");
        var clienteEsperado = new Cliente(Guid.NewGuid(), cpf, "Cliente Teste", "cliente@dominio.com");

        _clienteGatewayMock
            .Setup(gateway => gateway.GetClienteByCpfAsync(cpf))
            .ReturnsAsync(clienteEsperado);

        // Instância do UseCase com os mocks
        var useCase = new IdentificarClienteUseCaseTestWrapper(_loggerMock.Object, _clienteGatewayMock.Object);

        // Act
        var resultado = await useCase.PublicExecute(cpf);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(clienteEsperado, resultado);
        _clienteGatewayMock.Verify(gateway => gateway.GetClienteByCpfAsync(cpf), Times.Once);
    }

    [Fact]
    public async Task Execute_DeveRetornarNullQuandoCpfNaoExistente()
    {
        // Arrange
        var cpf = new Cpf("12345678901");

        _clienteGatewayMock
            .Setup(gateway => gateway.GetClienteByCpfAsync(cpf))
            .ReturnsAsync((Cliente?)null);

        // Instância do UseCase com os mocks
        var useCase = new IdentificarClienteUseCaseTestWrapper(_loggerMock.Object, _clienteGatewayMock.Object);

        // Act
        var resultado = await useCase.PublicExecute(cpf);

        // Assert
        Assert.Null(resultado);
        _clienteGatewayMock.Verify(gateway => gateway.GetClienteByCpfAsync(cpf), Times.Once);
    }


}


public class IdentificarClienteUseCaseTestWrapper : IdentificarClienteUseCase
{
    public IdentificarClienteUseCaseTestWrapper(ILogger<IdentificarClienteUseCase> logger, IClienteGateway clienteGateway)
        : base(logger, clienteGateway)
    {
    }

    public Task<Cliente?> PublicExecute(Cpf cpf)
    {
        return Execute(cpf);
    }
}
using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Clientes.Gateways;
using Pedidos.Apps.Clientes.UseCases;
using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Tests.UnitTests.Application.Clientes.UseCases;
public class IdentificarClienteUseCaseTests
{
    private readonly Mock<IClienteGateway> _clienteGatewayMock;
    private readonly Mock<ILogger<IdentificarClienteUseCase>> _loggerMock;
    private readonly IdentificarClienteUseCase _useCase;

    public IdentificarClienteUseCaseTests()
    {
        _clienteGatewayMock = new Mock<IClienteGateway>();
        _loggerMock = new Mock<ILogger<IdentificarClienteUseCase>>();
        _useCase = new IdentificarClienteUseCase(_loggerMock.Object, _clienteGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_DeveRetornarCliente_QuandoCpfExiste()
    {
        var cpf = new Cpf("123.456.789-09");
        var cliente = new Cliente(Guid.NewGuid(), cpf, "Cliente Teste", new EmailAddress("teste@email.com"));

        _clienteGatewayMock.Setup(g => g.GetClienteByCpfAsync(cpf)).ReturnsAsync(cliente);

        var resultado = await _useCase.ResolveAsync(cpf);

        Assert.NotNull(resultado);
      
    }

    //[Fact]
    //public async Task Execute_DeveRetornarNulo_QuandoCpfNaoExiste()
    //{
    //    var cpf = new Cpf("123.456.789-09");

    //    _clienteGatewayMock.Setup(g => g.GetClienteByCpfAsync(cpf)).ReturnsAsync((Cliente?)null);

    //    var resultado = await _useCase.ResolveAsync(cpf);

    //    Assert.Null(resultado);
    //}
}

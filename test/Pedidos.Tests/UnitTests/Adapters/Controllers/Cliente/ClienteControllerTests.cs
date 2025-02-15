using Moq;
using Pedidos.Adapters.Controllers.Clientes.Dtos;
using Pedidos.Adapters.Controllers.Clientes;
using Pedidos.Apps.Clientes.Gateways;
using Microsoft.Extensions.Logging;
using Pedidos.Domain.ValueObjects;


namespace Tests.UnitTests.Adapters.Controllers.Cliente;
public class ClienteControllerTests
{
    private readonly Mock<IClienteGateway> _clienteGatewayMock;
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly ClienteController _controller;

    public ClienteControllerTests()
    {
        _clienteGatewayMock = new Mock<IClienteGateway>();
        _loggerFactoryMock = new Mock<ILoggerFactory>();
        _controller = new ClienteController(_loggerFactoryMock.Object, _clienteGatewayMock.Object);
    }

    [Fact]
    public async Task IdentificarClienteAsync_DeveRetornarErro_QuandoCpfInvalido()
    {
        var resultado = await _controller.IdentificarClienteAsync("123");

        Assert.True(resultado.IsFailure);
        Assert.Contains(resultado.ProblemDetails, e => e.Detail == "Cpf inválido");
    }

  
    //[Fact]
    //public async Task IdentificarClienteAsync_DeveRetornarCliente_QuandoEncontrado()
    //{
    //    var cliente = new Pedidos.Domain.Clientes.Entities.Cliente(Guid.NewGuid(), "12345678901", "Nome Cliente", "email@teste.com");
    //    _clienteGatewayMock.Setup(x => x.GetClienteByCpfAsync(It.IsAny<Cpf>())).ReturnsAsync(cliente);

    //    var resultado = await _controller.IdentificarClienteAsync("12345678901");

    //    Assert.True(resultado.IsSucceed);
    //    Assert.Equal(cliente.Id, resultado.Value!.Id);
    //    Assert.Equal(cliente.Nome, resultado.Value.Nome);
    //}

    [Fact]
    public async Task CriarNovoClienteAsync_DeveRetornarErro_QuandoCpfOuEmailInvalido()
    {
        var novoCliente = new NovoClienteDto(Cpf: "123", Nome: "Teste", Email: "email");

        var resultado = await _controller.CriarNovoClienteAsync(novoCliente);

        Assert.True(resultado.IsFailure);
        Assert.Contains(resultado.ProblemDetails, e => e.Detail == "Cpf inválido");
        Assert.Contains(resultado.ProblemDetails, e => e.Detail == "Email inválido");
    }

    //[Fact]
    //public async Task CriarNovoClienteAsync_DeveCriarCliente_QuandoDadosValidos()
    //{
    //    var novoCliente = new NovoClienteDto(Cpf: "12345678901", Nome: "Teste", Email: "email@teste.com");
    //    var cliente = new Pedidos.Domain.Clientes.Entities.Cliente(Guid.NewGuid(), "12345678901", "Teste", "email@teste.com");

    //    _clienteGatewayMock.Setup(x => x.InsertCliente(It.IsAny<Pedidos.Domain.Clientes.Entities.Cliente>())).ReturnsAsync(cliente);

    //    var resultado = await _controller.CriarNovoClienteAsync(novoCliente);

    //    Assert.True(resultado.IsSucceed);
    //    Assert.Equal(cliente.Id, resultado.Value!.Id);
    //    Assert.Equal(cliente.Nome, resultado.Value.Nome);
    //}
}

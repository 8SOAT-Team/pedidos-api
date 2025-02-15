using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Clientes.Dtos;
using Pedidos.Apps.Clientes.Gateways;
using Pedidos.Apps.Clientes.UseCases;
using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.Exceptions;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Tests.UnitTests.Application.Clientes.UseCases;
public class CriarNovoClienteUseCaseTests
{
    private readonly Mock<IClienteGateway> _clienteGatewayMock;
    private readonly Mock<ILogger<CriarNovoClienteUseCase>> _loggerMock;
    private readonly CriarNovoClienteUseCase _useCase;

    public CriarNovoClienteUseCaseTests()
    {
        _clienteGatewayMock = new Mock<IClienteGateway>();
        _loggerMock = new Mock<ILogger<CriarNovoClienteUseCase>>();
        _useCase = new CriarNovoClienteUseCase(_loggerMock.Object, _clienteGatewayMock.Object);
    }

    [Fact]
    public async Task Execute_DeveCriarCliente_QuandoCpfNaoExiste()
    {
        var novoClienteDto = new CriarNovoClienteDto(new Cpf("123.456.789-09"), "Cliente Teste", new EmailAddress("teste@email.com"));
        var clienteCriado = new Cliente(Guid.NewGuid(), novoClienteDto.Cpf, novoClienteDto.Nome, novoClienteDto.Email);

        _clienteGatewayMock.Setup(g => g.GetClienteByCpfAsync(novoClienteDto.Cpf)).ReturnsAsync((Cliente?)null);
        _clienteGatewayMock.Setup(g => g.InsertCliente(It.IsAny<Cliente>())).ReturnsAsync(clienteCriado);

        var resultado = await _useCase.ResolveAsync(novoClienteDto);

        Assert.NotNull(resultado);
        Assert.Equal(novoClienteDto.Cpf, resultado.Value.Cpf);
        Assert.Equal(novoClienteDto.Nome, resultado.Value.Nome);
        Assert.Equal(novoClienteDto.Email, resultado.Value.Email);
    }

    //[Fact]
    //public async Task Execute_DeveRetornarNulo_QuandoCpfJaExiste()
    //{
    //    var novoClienteDto = new CriarNovoClienteDto(new Cpf("123.456.789-09"), "Cliente Teste", new EmailAddress("teste@email.com"));
    //    var clienteExistente = new Cliente(Guid.NewGuid(), novoClienteDto.Cpf, "Outro Cliente", new EmailAddress("outro@email.com"));

    //    _clienteGatewayMock.Setup(g => g.GetClienteByCpfAsync(novoClienteDto.Cpf)).ReturnsAsync(clienteExistente);

    //    var resultado = await _useCase.ResolveAsync(novoClienteDto);

    //    Assert.Null(resultado);
    //}

    [Fact]
    public async Task Execute_DeveRegistrarErro_QuandoCpfJaExiste()
    {
        var novoClienteDto = new CriarNovoClienteDto(new Cpf("123.456.789-09"), "Cliente Teste", new EmailAddress("teste@email.com"));
        var clienteExistente = new Cliente(Guid.NewGuid(), novoClienteDto.Cpf, "Outro Cliente", new EmailAddress("outro@email.com"));

        _clienteGatewayMock.Setup(g => g.GetClienteByCpfAsync(novoClienteDto.Cpf)).ReturnsAsync(clienteExistente);

        await _useCase.ResolveAsync(novoClienteDto);

        Assert.Contains(_useCase.GetErrors(), e => e.Description == "Cpf já cadastrado!");
    }

    //[Theory]
    //[InlineData("", "teste@email.com")]
    //[InlineData(null, "teste@email.com")]
    //[InlineData("Jo", "teste@email.com")]
    //public async Task Execute_DeveLancarExcecao_QuandoNomeInvalido(string nome, string email)
    //{
    //    var novoClienteDto = new CriarNovoClienteDto(new Cpf("123.456.789-09"), nome, new EmailAddress(email));

    //    await Assert.ThrowsAsync<DomainExceptionValidation>(() => _useCase.ResolveAsync(novoClienteDto));
    //}
}
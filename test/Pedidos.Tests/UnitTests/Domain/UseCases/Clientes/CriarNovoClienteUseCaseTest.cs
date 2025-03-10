﻿using Microsoft.Extensions.Logging;
using Moq;
using Pedidos.Apps.Clientes.Dtos;
using Pedidos.Apps.Clientes.Gateways;
using Pedidos.Apps.Clientes.UseCases;
using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Tests.UnitTests.Domain.UseCases.Clientes;

public class CriarNovoClienteUseCaseTest
{
    private readonly Mock<ILogger<CriarNovoClienteUseCase>> _loggerMock;
    private readonly Mock<IClienteGateway> _clienteGatewayMock;

    public CriarNovoClienteUseCaseTest()
    {
        _loggerMock = new Mock<ILogger<CriarNovoClienteUseCase>>();
        _clienteGatewayMock = new Mock<IClienteGateway>();
    }

    [Fact]
    public async Task Execute_ClienteJaCadastrado_DeveRetornarErro()
    {
        // Arrange
        var cpfExistente = new Cpf("12345678901");
        var novoClienteDto = new CriarNovoClienteDto(
            cpfExistente,
            "Cliente Existente",
            new EmailAddress("cliente@dominio.com")
        );

        var clienteExistente = new Cliente(Guid.NewGuid(), cpfExistente, "Cliente Existente", "cliente@dominio.com");

        _clienteGatewayMock.Setup(gateway => gateway.GetClienteByCpfAsync(cpfExistente))
            .ReturnsAsync(clienteExistente);

        var useCase = new CriarNovoClienteUseCaseTestWrapper(_loggerMock.Object, _clienteGatewayMock.Object);

        // Act
        var resultado = await useCase.PublicExecute(novoClienteDto);

        // Assert
        Assert.Null(resultado);
        IReadOnlyCollection<CleanArch.UseCase.Faults.UseCaseError> useCaseErrors = useCase.GetErrors();
        Assert.Single(useCaseErrors);
        Assert.Equal("Cpf já cadastrado!", useCaseErrors.FirstOrDefault().Description);
    }

    [Fact]
    public async Task Execute_ClienteNaoCadastrado_DeveCriarCliente()
    {
        // Arrange
        var cpfNovo = new Cpf("98765432100");
        var novoClienteDto = new CriarNovoClienteDto(
            cpfNovo,
            "Novo Cliente",
            new EmailAddress("novocliente@dominio.com")
        );

        _clienteGatewayMock.Setup(gateway => gateway.GetClienteByCpfAsync(cpfNovo))
            .ReturnsAsync((Cliente)null);

        var clienteInserido = new Cliente(Guid.NewGuid(), cpfNovo, novoClienteDto.Nome, novoClienteDto.Email.Address);
        _clienteGatewayMock.Setup(gateway => gateway.InsertCliente(It.IsAny<Cliente>()))
            .ReturnsAsync(clienteInserido);

        var useCase = new CriarNovoClienteUseCaseTestWrapper(_loggerMock.Object, _clienteGatewayMock.Object);

        // Act
        var resultado = await useCase.PublicExecute(novoClienteDto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(novoClienteDto.Nome, resultado.Nome);
        Assert.Equal(novoClienteDto.Email.Address, resultado.Email);
    }
}


public class CriarNovoClienteUseCaseTestWrapper : CriarNovoClienteUseCase
{
    public CriarNovoClienteUseCaseTestWrapper(ILogger<CriarNovoClienteUseCase> logger, IClienteGateway clienteGateway)
        : base(logger, clienteGateway)
    {
    }

    public Task<Cliente?> PublicExecute(CriarNovoClienteDto novoCliente)
    {
        return Execute(novoCliente);
    }
}
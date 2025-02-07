using Pedidos.Apps.Clientes.Dtos;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Tests.UnitTests.Domain.UseCases.Clientes.Dtos;

public class CriarNovoClienteDtoTest
{
    [Fact]
    public void CriarNovoClienteDto_DeveValidarValores()
    {
        // Arrange
        var cpf = new Cpf("12345678901");
        var nome = "João Silva";
        var email = new EmailAddress("joao.silva@example.com");

        // Instanciando o Dto
        var clienteDto = new CriarNovoClienteDto(cpf, nome, email);

        // Act & Assert
        Assert.Equal(cpf, clienteDto.Cpf);
        Assert.Equal(nome, clienteDto.Nome);
        Assert.Equal(email, clienteDto.Email);
    }
}

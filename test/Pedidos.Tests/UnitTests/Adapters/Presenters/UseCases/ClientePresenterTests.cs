using Pedidos.Adapters.Presenters.Clientes;
using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.Exceptions;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Tests.UnitTests.Adapters.Presenters.UseCases;
public class ClientePresenterTests
{
    [Fact]
    public void AdaptClienteIdentificado_Deve_Retornar_Dados_Corretamente()
    {
        // Arrange
        var cliente = new Cliente(Guid.NewGuid(), "12345678901", "João Silva", "joao@email.com");

        // Act
        var dto = ClientePresenter.AdaptClienteIdentificado(cliente);

        // Assert
        Assert.Equal(cliente.Id, dto.Id);
        Assert.Equal(cliente.Nome, dto.Nome);
    }

    [Fact]
    public void AdaptCliente_Deve_Retornar_Dados_Corretamente()
    {
        // Arrange
        var cliente = new Cliente(Guid.NewGuid(), "12345678901", "Maria Oliveira", "maria@email.com");

        // Act
        var dto = ClientePresenter.AdaptCliente(cliente);

        // Assert
        Assert.Equal(cliente.Id, dto.Id);
        Assert.Equal(cliente.Nome, dto.Nome);
        Assert.Equal(cliente.Email.Address, dto.Email);
        Assert.Equal(cliente.Cpf.Value, dto.Cpf);
    }

    [Fact]
    public void AdaptClienteIdentificado_Deve_LancarExcecao_Quando_Cliente_Null()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => ClientePresenter.AdaptClienteIdentificado(null!));
    }

    [Fact]
    public void AdaptCliente_Deve_LancarExcecao_Quando_Cliente_Null()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => ClientePresenter.AdaptCliente(null!));
    }

    [Fact]
    public void Cliente_Deve_Ser_Inicializado_Corretamente()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cpf = new Cpf("12345678901");
        var email = new EmailAddress("teste@email.com");
        var nome = "Carlos Souza";

        // Act
        var cliente = new Cliente(id, cpf, nome, email);

        // Assert
        Assert.Equal(id, cliente.Id);
        Assert.Equal(nome, cliente.Nome);
        Assert.Equal(cpf, cliente.Cpf);
        Assert.Equal(email, cliente.Email);
    }

    [Fact]
    public void ChangeNome_Deve_Alterar_Nome_Corretamente()
    {
        // Arrange
        var cliente = new Cliente("12345678901", "Ana Lima", "ana@email.com");

        // Act
        cliente.ChangeNome("Ana Souza");

        // Assert
        Assert.Equal("Ana Souza", cliente.Nome);
    }

    [Fact]
    public void ChangeNome_Deve_LancarExcecao_Para_Nome_Invalido()
    {
        // Arrange
        var cliente = new Cliente("12345678901", "Lucas Mendes", "lucas@email.com");

        // Act & Assert
        Assert.Throws<DomainExceptionValidation>(() => cliente.ChangeNome(""));
        Assert.Throws<DomainExceptionValidation>(() => cliente.ChangeNome("AB"));
    }

    [Fact]
    public void ChangeEmail_Deve_Alterar_Email_Corretamente()
    {
        // Arrange
        var cliente = new Cliente("12345678901", "Fernanda Costa", "fernanda@email.com");
        var novoEmail = new EmailAddress("nova@email.com");

        // Act
        cliente.ChangeEmail(novoEmail);

        // Assert
        Assert.Equal(novoEmail, cliente.Email);
    }

    [Fact]
    public void Cliente_Deve_LancarExcecao_Para_Cpf_Invalido()
    {
        // Act & Assert
        Assert.Throws<DomainExceptionValidation>(() => new Cliente("", "Rafael Dias", "rafael@email.com"));
        Assert.Throws<DomainExceptionValidation>(() => new Cliente("123", "Rafael Dias", "rafael@email.com"));
    }
}


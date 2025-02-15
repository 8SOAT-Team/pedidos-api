using Pedidos.Adapters.Controllers.Clientes.Dtos;

namespace Pedidos.Tests.UnitTests.Adapters.Controllers.Cliente.Dtos;
public class ClienteDtoTests
{
    [Fact]
    public void ClienteDto_DeveCriarInstanciaComValoresCorretos()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "João Silva";
        var email = "joao@email.com";
        var cpf = "12345678901";

        // Act
        var cliente = new ClienteDto { Id = id, Nome = nome, Email = email, Cpf = cpf };

        // Assert
        Assert.Equal(id, cliente.Id);
        Assert.Equal(nome, cliente.Nome);
        Assert.Equal(email, cliente.Email);
        Assert.Equal(cpf, cliente.Cpf);
    }

    //[Fact]
    //public void ClienteDto_NaoDevePermitirValoresNulos()
    //{
    //    // Arrange
    //    var id = Guid.NewGuid();

    //    // Act & Assert
    //    Assert.Throws<ArgumentNullException>(() => new ClienteDto { Id = id, Nome = null!, Email = "email@test.com", Cpf = "12345678901" });
    //    Assert.Throws<ArgumentNullException>(() => new ClienteDto { Id = id, Nome = "João", Email = null!, Cpf = "12345678901" });
    //    Assert.Throws<ArgumentNullException>(() => new ClienteDto { Id = id, Nome = "João", Email = "email@test.com", Cpf = null! });
    //}

    [Fact]
    public void ClienteDto_DeveTerIdUnico()
    {
        // Arrange
        var cliente1 = new ClienteDto { Id = Guid.NewGuid(), Nome = "João", Email = "joao@email.com", Cpf = "12345678901" };
        var cliente2 = new ClienteDto { Id = Guid.NewGuid(), Nome = "Maria", Email = "maria@email.com", Cpf = "98765432100" };

        // Assert
        Assert.NotEqual(cliente1.Id, cliente2.Id);
    }

    [Fact]
    public void ClienteDto_DeveAceitarCpfValido()
    {
        // Arrange
        var cpf = "12345678901";

        // Act
        var cliente = new ClienteDto { Id = Guid.NewGuid(), Nome = "Carlos", Email = "carlos@email.com", Cpf = cpf };

        // Assert
        Assert.Equal(cpf, cliente.Cpf);
    }

    [Fact]
    public void ClienteDto_DeveAceitarEmailValido()
    {
        // Arrange
        var email = "cliente@email.com";

        // Act
        var cliente = new ClienteDto { Id = Guid.NewGuid(), Nome = "Cliente", Email = email, Cpf = "12345678901" };

        // Assert
        Assert.Equal(email, cliente.Email);
    }

    [Fact]
    public void ClienteDto_DeveTerMesmoHashParaMesmoId()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cliente1 = new ClienteDto { Id = id, Nome = "Ana", Email = "ana@email.com", Cpf = "12345678901" };
        var cliente2 = new ClienteDto { Id = id, Nome = "Ana", Email = "ana@email.com", Cpf = "12345678901" };

        // Assert
        Assert.Equal(cliente1.GetHashCode(), cliente2.GetHashCode());
    }

    [Fact]
    public void ClienteDto_DeveRetornarDiferenteHashParaDiferentesClientes()
    {
        // Arrange
        var cliente1 = new ClienteDto { Id = Guid.NewGuid(), Nome = "Ana", Email = "ana@email.com", Cpf = "12345678901" };
        var cliente2 = new ClienteDto { Id = Guid.NewGuid(), Nome = "Pedro", Email = "pedro@email.com", Cpf = "98765432100" };

        // Assert
        Assert.NotEqual(cliente1.GetHashCode(), cliente2.GetHashCode());
    }

    [Fact]
    public void ClienteDto_DevePermitirCriacaoComValoresValidos()
    {
        // Arrange
        var cliente = new ClienteDto { Id = Guid.NewGuid(), Nome = "Lucas", Email = "lucas@email.com", Cpf = "12345678901" };

        // Assert
        Assert.NotNull(cliente);
    }

    //[Fact]
    //public void ClienteDto_DeveTerPropriedadesImutaveis()
    //{
    //    // Arrange
    //    var cliente = new ClienteDto { Id = Guid.NewGuid(), Nome = "Carlos", Email = "carlos@email.com", Cpf = "12345678901" };

    //    // Assert
    //    Assert.Throws<InvalidOperationException>(() => cliente = cliente with { Nome = "Novo Nome" });
    //}
}

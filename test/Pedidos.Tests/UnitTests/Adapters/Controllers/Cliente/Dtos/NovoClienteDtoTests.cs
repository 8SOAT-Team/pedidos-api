using Pedidos.Adapters.Controllers.Clientes.Dtos;
using Pedidos.Domain.Exceptions;

namespace Pedidos.Tests.UnitTests.Adapters.Controllers.Cliente.Dtos;
public class NovoClienteDtoTests
{
    [Fact]
    public void CriarNovoClienteDto_DeveCriarComValoresCorretos()
    {
        // Arrange
        var cpf = "12345678901";
        var nome = "João Silva";
        var email = "joao.silva@example.com";

        // Act
        var cliente = new NovoClienteDto(cpf, nome, email);

        // Assert
        Assert.Equal(cpf, cliente.Cpf);
        Assert.Equal(nome, cliente.Nome);
        Assert.Equal(email, cliente.Email);
    }

    [Fact]
    public void CriarNovoClienteDto_DeveManterImutabilidade()
    {
        // Arrange
        var clienteOriginal = new NovoClienteDto("12345678901", "João Silva", "joao.silva@example.com");

        // Act & Assert
        Assert.Throws<DomainExceptionValidation>(() => clienteOriginal = clienteOriginal with { Cpf = "98765432100" });
        Assert.Throws<DomainExceptionValidation>(() => clienteOriginal = clienteOriginal with { Nome = "Maria Souza" });
        Assert.Throws<DomainExceptionValidation>(() => clienteOriginal = clienteOriginal with { Email = "maria.souza@example.com" });
    }

    [Fact]
    public void NovoClienteDto_Igualdade_DeveRetornarTrueParaObjetosIguais()
    {
        // Arrange
        var cliente1 = new NovoClienteDto("12345678901", "João Silva", "joao.silva@example.com");
        var cliente2 = new NovoClienteDto("12345678901", "João Silva", "joao.silva@example.com");

        // Act & Assert
        Assert.True(cliente1.Equals(cliente2));
    }

    [Fact]
    public void NovoClienteDto_Igualdade_DeveRetornarFalseParaObjetosDiferentes()
    {
        // Arrange
        var cliente1 = new NovoClienteDto("12345678901", "João Silva", "joao.silva@example.com");
        var cliente2 = new NovoClienteDto("98765432100", "Maria Souza", "maria.souza@example.com");

        // Act & Assert
        Assert.False(cliente1.Equals(cliente2));
    }

    [Fact]
    public void NovoClienteDto_ToString_DeveRetornarStringCorreta()
    {
        // Arrange
        var cliente = new NovoClienteDto("12345678901", "João Silva", "joao.silva@example.com");

        // Act
        var resultado = cliente.ToString();

        // Assert
        Assert.Contains("12345678901", resultado);
        Assert.Contains("João Silva", resultado);
        Assert.Contains("joao.silva@example.com", resultado);
    }

    [Fact]
    public void NovoClienteDto_SemEmail_DevePermitirInstanciacaoSemEmail()
    {
        // Arrange
        var cliente = new NovoClienteDto("12345678901", "João Silva", null);

        // Act & Assert
        Assert.Null(cliente.Email);
    }

    [Fact]
    public void NovoClienteDto_SemNome_DevePermitirInstanciacaoSemNome()
    {
        // Arrange
        var cliente = new NovoClienteDto("12345678901", null, "joao.silva@example.com");

        // Act & Assert
        Assert.Null(cliente.Nome);
    }

    [Fact]
    public void NovoClienteDto_SemCpf_DevePermitirInstanciacaoSemCpf()
    {
        // Arrange
        var cliente = new NovoClienteDto(null, "João Silva", "joao.silva@example.com");

        // Act & Assert
        Assert.Null(cliente.Cpf);
    }

    [Fact]
    public void NovoClienteDto_CpfDeveSerString()
    {
        // Arrange
        var cliente = new NovoClienteDto("12345678901", "João Silva", "joao.silva@example.com");

        // Act & Assert
        Assert.IsType<string>(cliente.Cpf);
    }

    [Fact]
    public void NovoClienteDto_CompararComNull_DeveRetornarFalse()
    {
        // Arrange
        var cliente = new NovoClienteDto("12345678901", "João Silva", "joao.silva@example.com");

        // Act & Assert
        Assert.False(cliente.Equals(null));
    }
}
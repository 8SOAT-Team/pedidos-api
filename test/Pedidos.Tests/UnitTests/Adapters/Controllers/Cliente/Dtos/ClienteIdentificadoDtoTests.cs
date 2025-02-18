using Pedidos.Adapters.Controllers.Clientes.Dtos;

namespace Pedidos.Tests.UnitTests.Adapters.Controllers.Cliente.Dtos;
public class ClienteIdentificadoDtoTests
{
    [Fact]
    public void CriarClienteIdentificadoDto_DeveDefinirPropriedadesCorretamente()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Cliente Teste";

        // Act
        var dto = new ClienteIdentificadoDto(id, nome);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(nome, dto.Nome);
    }

    [Fact]
    public void DoisDtosComMesmoIdENome_DevemSerIguais()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Cliente Teste";

        // Act
        var dto1 = new ClienteIdentificadoDto(id, nome);
        var dto2 = new ClienteIdentificadoDto(id, nome);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
    }

    [Fact]
    public void DoisDtosComIdsDiferentes_DevemSerDiferentes()
    {
        // Arrange
        var dto1 = new ClienteIdentificadoDto(Guid.NewGuid(), "Cliente A");
        var dto2 = new ClienteIdentificadoDto(Guid.NewGuid(), "Cliente A");

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
    }

    [Fact]
    public void DoisDtosComNomesDiferentes_DevemSerDiferentes()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto1 = new ClienteIdentificadoDto(id, "Cliente A");
        var dto2 = new ClienteIdentificadoDto(id, "Cliente B");

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
    }

    [Fact]
    public void CriarDto_ComNomeNulo_DevePermitir()
    {
        // Arrange
        var id = Guid.NewGuid();
        string? nome = null;

        // Act
        var dto = new ClienteIdentificadoDto(id, nome!);

        // Assert
        Assert.Null(dto.Nome);
    }

    [Fact]
    public void CriarDto_ComIdVazio_DevePermitir()
    {
        // Arrange
        var dto = new ClienteIdentificadoDto(Guid.Empty, "Cliente Teste");

        // Assert
        Assert.Equal(Guid.Empty, dto.Id);
    }

    [Fact]
    public void HashCode_DeveSerIgualParaDtosComMesmoIdENome()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Cliente Teste";

        var dto1 = new ClienteIdentificadoDto(id, nome);
        var dto2 = new ClienteIdentificadoDto(id, nome);

        // Assert
        Assert.Equal(dto1.GetHashCode(), dto2.GetHashCode());
    }

    [Fact]
    public void HashCode_DeveSerDiferenteParaDtosComIdsDiferentes()
    {
        // Arrange
        var dto1 = new ClienteIdentificadoDto(Guid.NewGuid(), "Cliente Teste");
        var dto2 = new ClienteIdentificadoDto(Guid.NewGuid(), "Cliente Teste");

        // Assert
        Assert.NotEqual(dto1.GetHashCode(), dto2.GetHashCode());
    }

    [Fact]
    public void ToString_DeveRetornarStringCorreta()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "Cliente Teste";
        var dto = new ClienteIdentificadoDto(id, nome);

        // Act
        var result = dto.ToString();

        // Assert
        Assert.Contains(id.ToString(), result);
        Assert.Contains(nome, result);
    }

    [Fact]
    public void Igualdade_DeveSerFalsaAoCompararComNull()
    {
        // Arrange
        var dto = new ClienteIdentificadoDto(Guid.NewGuid(), "Cliente Teste");

        // Assert
        Assert.False(dto.Equals(null));
    }
}

using Pedidos.Apps.Clientes.Dtos;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Tests.UnitTests.Application.Clientes.Dtos;
public class CriarNovoClienteDtoTests
{
    [Fact]
    public void CriarNovoClienteDto_DeveCriarComValoresValidos()
    {
        var cpf = new Cpf("123.456.789-09");
        var email = new EmailAddress("teste@email.com");
        var dto = new CriarNovoClienteDto(cpf, "Cliente Teste", email);

        Assert.Equal(cpf, dto.Cpf);
        Assert.Equal("Cliente Teste", dto.Nome);
        Assert.Equal(email, dto.Email);
    }

    [Fact]
    public void CriarNovoClienteDto_DeveManterValoresAoCriar()
    {
        var dto1 = new CriarNovoClienteDto(new Cpf("123.456.789-09"), "Cliente Teste", new EmailAddress("teste@email.com"));
        var dto2 = new CriarNovoClienteDto(dto1.Cpf, dto1.Nome, dto1.Email);

        Assert.Equal(dto1, dto2);
    }

    //[Theory]
    //[InlineData("")]
    //[InlineData(null)]
    //public void CriarNovoClienteDto_DeveLancarExcecaoSeNomeInvalido(string nome)
    //{
    //    var cpf = new Cpf("123.456.789-09");
    //    var email = new EmailAddress("teste@email.com");

    //    Assert.Throws<ArgumentException>(() => new CriarNovoClienteDto(cpf, nome, email));
    //}

    //[Theory]
    //[InlineData("111.111.111-11")]
    //[InlineData("000.000.000-00")]
    //public void CriarNovoClienteDto_DeveLancarExcecaoSeCpfInvalido(string cpfInvalido)
    //{
    //    var email = new EmailAddress("teste@email.com");

    //    Assert.Throws<ArgumentException>(() => new CriarNovoClienteDto(new Cpf(cpfInvalido), "Cliente Teste", email));
    //}

    //[Theory]
    //[InlineData("email_invalido")]
    //[InlineData("@email.com")]
    //[InlineData("teste@.com")]
    //public void CriarNovoClienteDto_DeveLancarExcecaoSeEmailInvalido(string emailInvalido)
    //{
    //    var cpf = new Cpf("123.456.789-09");

    //    Assert.Throws<ArgumentException>(() => new CriarNovoClienteDto(cpf, "Cliente Teste", new EmailAddress(emailInvalido)));
    //}

    [Fact]
    public void CriarNovoClienteDto_DeveSerImutavel()
    {
        var cpf = new Cpf("123.456.789-09");
        var email = new EmailAddress("teste@email.com");
        var dto = new CriarNovoClienteDto(cpf, "Cliente Teste", email);

        var novoDto = dto with { Nome = "Novo Nome" };

        Assert.NotEqual(dto, novoDto);
        Assert.Equal("Novo Nome", novoDto.Nome);
        Assert.Equal(dto.Cpf, novoDto.Cpf);
        Assert.Equal(dto.Email, novoDto.Email);
    }

    [Fact]
    public void CriarNovoClienteDto_DeveSerIgualParaValoresIguais()
    {
        var cpf = new Cpf("123.456.789-09");
        var email = new EmailAddress("teste@email.com");
        var dto1 = new CriarNovoClienteDto(cpf, "Cliente Teste", email);
        var dto2 = new CriarNovoClienteDto(cpf, "Cliente Teste", email);

        Assert.Equal(dto1, dto2);
    }

    [Fact]
    public void CriarNovoClienteDto_DeveSerDiferenteParaValoresDiferentes()
    {
        var dto1 = new CriarNovoClienteDto(new Cpf("123.456.789-09"), "Cliente Teste", new EmailAddress("teste@email.com"));
        var dto2 = new CriarNovoClienteDto(new Cpf("987.654.321-00"), "Outro Cliente", new EmailAddress("outro@email.com"));

        Assert.NotEqual(dto1, dto2);
    }
}


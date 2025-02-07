using Bogus;
using Pedidos.Domain.Exceptions;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Domain.Produtos.Enums;

namespace Pedidos.Tests.UnitTests.Domain.Entities;
public class ProdutoTest
{
    private readonly Faker _faker = new();
    
    [Fact]
    public void DeveRetornarExceptionQuandoNomeForNulo()
    {
        //Arrange
        var nome = "";
        var descricao = "Teste";
        //Act
        Action act = () => new Produto(nome, descricao, 10, "http://Imagemdeteste", _faker.PickRandom<ProdutoCategoria>());
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }
    [Fact]
    public void DeveRetornarExceptionQuandoNomeForMenorQue3Caracteres()
    {
        //Arrange
        var nome = "Te";
        var descricao = "Teste";
        //Act
        Action act = () => new Produto(nome, descricao, 10, "http://Imagemdeteste", _faker.PickRandom<ProdutoCategoria>());
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }

    [Fact]
    public void DeveRetornarExceptionQuandoImagemInvalida()
    {
        //Arrange
        var nome = "Batata Frita";
        var descricao = "Teste";
        //Act
        Action act = () => new Produto(nome, descricao, 10, "Imagem de teste", _faker.PickRandom<ProdutoCategoria>());
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }

    [Fact]
    public void DeveCriarProdutoComSucesso()
    {
        //Arrange
        var nome = "Teste";
        var descricao = "Teste";
        //Act
        var produto = new Produto(nome, descricao, 10, "https://imagemdeteste", _faker.PickRandom<ProdutoCategoria>());
        //Assert
        Assert.NotNull(produto);
    }

    [Theory]
    [InlineData("Teste", "Teste", 10, "https://imagemdeteste1", ProdutoCategoria.Lanche)]
    [InlineData("Teste2", "Teste2", 10, "http://imagemdeteste2", ProdutoCategoria.Lanche)]
    [InlineData("Teste3", "Teste3", 10, "http://imagemdeteste3", ProdutoCategoria.Lanche)]
    public void DeveCriarProdutoComSucessoParametrizado(string nome, string descricao, decimal preco, string imagem, ProdutoCategoria categoria)
    {
        //Arrange
        //Act
        var produto = new Produto(nome, descricao, preco, imagem, categoria);
        //Assert
        Assert.NotNull(produto);
    }

    [Fact]
    public void DeveRetornarExceptionQuandoImagemForNula()
    {
        //Arrange
        var nome = "Teste";
        var descricao = "Teste";
        //Act
        Action act = () => new Produto(nome, descricao, 10, "", _faker.PickRandom<ProdutoCategoria>());
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }

    [Fact]
    public void DeveRetornarExceptionQuandoImagemForMenorQue3Caracteres()
    {
        //Arrange
        var nome = "Teste";
        var descricao = "Teste";
        //Act
        Action act = () => new Produto(nome, descricao, 10, "Im", _faker.PickRandom<ProdutoCategoria>());
        //Assert
        Assert.Throws<DomainExceptionValidation>(() => act());
    }

    [Fact]
    public void RenameTo_DadoQueONomeEhValido_DeveRenomear()
    {
        // Arrange
        var novoNome = "Novo Nome";
        var produto = new Produto("Nome Antigo", "Descricao", 10, "https://ImageUrl", _faker.PickRandom<ProdutoCategoria>());

        // Act
        produto.RenameTo(novoNome);

        // Assert
        Assert.Equal(produto.Nome, novoNome);
    }
}

using FluentAssertions;
using Pedidos.Domain.Produtos.Enums;
using Pedidos.Domain.Exceptions;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Adapters.Presenters.Produtos;

namespace Tests.UnitTests.Adapters.Presenters.UseCases;
public class ProdutoPresenterTests
{
    [Fact]
    public void AdaptProdutoCriado_DeveRetornarDtoCorreto()
    {
        // Arrange
        var produto = new Produto(
            "Hamburguer Artesanal",
            "Hamburguer com pão brioche e carne 200g",
            25.99m,
            "https://example.com/hamburguer.jpg",
            ProdutoCategoria.Lanche
        );

        // Act
        var dto = ProdutoPresenter.AdaptProdutoCriado(produto);

        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(produto.Id);
        dto.Nome.Should().Be(produto.Nome);
        dto.Descricao.Should().Be(produto.Descricao);
        dto.Preco.Should().Be(produto.Preco);
        dto.Imagem.Should().Be(produto.Imagem);
        dto.Categoria.Should().Be((Pedidos.Apps.Produtos.Enums.ProdutoCategoria)produto.Categoria);
    }

    [Fact]
    public void AdaptProduto_DeveRetornarDtoCorreto()
    {
        // Arrange
        var produto = new Produto(
            "Refrigerante",
            "Coca-Cola 350ml",
            5.99m,
            "https://example.com/coca.jpg",
            ProdutoCategoria.Bebida
        );

        // Act
        var dto = ProdutoPresenter.AdaptProduto(produto);

        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(produto.Id);
        dto.Nome.Should().Be(produto.Nome);
        dto.Descricao.Should().Be(produto.Descricao);
        dto.Preco.Should().Be(produto.Preco);
        dto.Imagem.Should().Be(produto.Imagem);
        dto.Categoria.Should().Be((Pedidos.Apps.Produtos.Enums.ProdutoCategoria)produto.Categoria);
    }

    [Fact]
    public void AdaptProduto_DeveConverterListaDeProdutosParaListaDeDtos()
    {
        // Arrange
        var produtos = new List<Produto>
            {
                new Produto("Batata Frita", "Batata frita crocante", 12.50m, "https://example.com/batata.jpg", ProdutoCategoria.Acompanhamento),
                new Produto("Sorvete", "Sorvete de chocolate 150ml", 7.99m, "https://example.com/sorvete.jpg", ProdutoCategoria.Sobremesa)
            };

        // Act
        var dtos = ProdutoPresenter.AdaptProduto(produtos);

        // Assert
        dtos.Should().NotBeNull().And.HaveCount(produtos.Count);
        dtos.Should().ContainSingle(dto => dto.Id == produtos[0].Id && dto.Nome == produtos[0].Nome);
        dtos.Should().ContainSingle(dto => dto.Id == produtos[1].Id && dto.Nome == produtos[1].Nome);
    }

    [Fact]
    public void CriarProduto_ComDadosInvalidos_DeveLancarExcecao()
    {
        // Act & Assert
        FluentActions.Invoking(() =>
            new Produto("", "Descrição válida", 10m, "https://example.com/imagem.jpg", ProdutoCategoria.Acompanhamento)
        ).Should().Throw<DomainExceptionValidation>()
        .WithMessage("Nome é obrigatório");

        FluentActions.Invoking(() =>
            new Produto("Produto", "", 10m, "https://example.com/imagem.jpg", ProdutoCategoria.Lanche)
        ).Should().Throw<DomainExceptionValidation>()
        .WithMessage("Descrição é obrigatória");

        FluentActions.Invoking(() =>
            new Produto("Produto", "Descrição válida", -1m, "https://example.com/imagem.jpg", ProdutoCategoria.Lanche)
        ).Should().Throw<DomainExceptionValidation>()
        .WithMessage("Preço inválido");

        FluentActions.Invoking(() =>
            new Produto("Produto", "Descrição válida", 10m, "imagem_invalida", ProdutoCategoria.Lanche)
        ).Should().Throw<DomainExceptionValidation>()
        .WithMessage("URL da imagem inválida.");
    }
}
using System.Text.Json.Serialization;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Exceptions;
using Pedidos.Domain.Produtos.Enums;

namespace Pedidos.Domain.Produtos.Entities;

public class Produto : Entity, IAggregateRoot
{
    protected Produto()
    {
    }

    public Produto(string nome, string descricao, decimal preco, string imagem, ProdutoCategoria categoria)
        : this(Guid.NewGuid(), nome, descricao, preco, imagem, categoria)
    {
    }

    [JsonConstructor]
    public Produto(Guid id, string nome, string descricao, decimal preco, string imagem, ProdutoCategoria categoria)
    {
        DomainExceptionValidation.When(id == Guid.Empty, "Id inválido");
        ValidationDomain(nome, descricao, preco, imagem, categoria);

        Id = id;
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Imagem = imagem;
        Categoria = categoria;
    }

    public string Nome { get; private set; } = null!;
    public string Descricao { get; private set; } = null!;
    public decimal Preco { get; private set; }
    public ProdutoCategoria Categoria { get; set; }
    public string Imagem { get; private set; } = null!;

    private static void ValidationDomain(string nome, string descricao, decimal preco, string imagem,
        ProdutoCategoria categoria)
    {
        ValidateDomainNome(nome);
        ValidateDomainDescricao(descricao);
        ValidateDomainImagem(imagem);
        ValidateDomainCategoria(categoria);
        ValidateDomainPreco(preco);
    }

    private static void ValidateDomainNome(string nome)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório");
        DomainExceptionValidation.When(nome!.Length < 3, "Nome deve ter no mínimo 3 caracteres");
        DomainExceptionValidation.When(nome.Length > 100, "Nome deve ter no máximo 100 caracteres");
    }

    private static void ValidateDomainDescricao(string descricao)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(descricao), "Descrição é obrigatória");

        DomainExceptionValidation.When(descricao!.Length < 3, "Descrição deve ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(descricao.Length > 100, "Descrição deve ter no máximo 100 caracteres");
    }

    private static void ValidateDomainImagem(string imagem)
    {
        DomainExceptionValidation.When(imagem!.Length < 3, "Endereço da imagem deve ter no mínimo 3 caracteres");
        DomainExceptionValidation.When(imagem.Length > 300, "Endereço da imagem deve ter no máximo 300 caracteres");
        DomainExceptionValidation.When(Uri.IsWellFormedUriString(imagem, UriKind.Absolute) is false,
            "URL da imagem inválida.");
    }

    private static void ValidateDomainCategoria(ProdutoCategoria categoria)
    {
        DomainExceptionValidation.When(Enum.IsDefined(categoria) is false, "Id inválido");
    }

    private static void ValidateDomainPreco(decimal preco)
    {
        DomainExceptionValidation.When(preco < 0, "Preço inválido");
    }

    public void RenameTo(string nome)
    {
        ValidateDomainNome(nome);
        Nome = nome;
    }

    public void DescribeAs(string descricao)
    {
        ValidateDomainDescricao(descricao);
        Descricao = descricao;
    }

    public void SetPreco(decimal preco)
    {
        ValidateDomainPreco(preco);
        Preco = preco;
    }

    public void SubstituteImagem(string imagem)
    {
        ValidateDomainImagem(imagem);
        Imagem = imagem;
    }

    public void ChangeToCategory(ProdutoCategoria categoria)
    {
        ValidateDomainCategoria(categoria);
        Categoria = categoria;
    }
}
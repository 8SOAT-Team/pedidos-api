using CleanArch.UseCase.Faults;
using Pedidos.Apps.Produtos.Enums;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Apps.UseCases;
using Pedidos.Domain.Produtos.Entities;

namespace Pedidos.Apps.Produtos.UseCases;

public class
    ListarProdutoPorCategoriaUseCase : UseCase<ListarProdutoPorCategoriaUseCase, ProdutoCategoria, ICollection<Produto>>
{
    private readonly IProdutoGateway _produtoGateway;

    public ListarProdutoPorCategoriaUseCase(ILogger<ListarProdutoPorCategoriaUseCase> logger,
        IProdutoGateway produtoGateway) : base(logger)
    {
        _produtoGateway = produtoGateway;
    }

    protected override async Task<ICollection<Produto>?> Execute(ProdutoCategoria command)
    {
        if (Enum.IsDefined(command)) return await _produtoGateway.GetProdutosByCategoriaAsync(command);
        AddError(new UseCaseError(UseCaseErrorType.BadRequest, "CategoriaId não pode ser vazio"));
        return null!;
    }
}
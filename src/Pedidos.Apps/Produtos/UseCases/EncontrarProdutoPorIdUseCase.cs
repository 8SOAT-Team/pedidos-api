using CleanArch.UseCase.Faults;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Apps.UseCases;
using Pedidos.Domain.Produtos.Entities;

namespace Pedidos.Apps.Produtos.UseCases;

public class EncontrarProdutoPorIdUseCase : UseCase<EncontrarProdutoPorIdUseCase, Guid, Produto>
{
    private readonly IProdutoGateway _produtoGateway;

    public EncontrarProdutoPorIdUseCase(ILogger<EncontrarProdutoPorIdUseCase> logger, IProdutoGateway produtoGateway) :
        base(logger)
    {
        _produtoGateway = produtoGateway;
    }

    protected override async Task<Produto?> Execute(Guid id)
    {
        if (id != Guid.Empty) return await _produtoGateway.GetProdutoByIdAsync(id);
        AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Id do produto não pode ser vazio."));
        return null!;
    }
}
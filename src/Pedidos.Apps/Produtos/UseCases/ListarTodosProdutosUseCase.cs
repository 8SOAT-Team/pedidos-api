using CleanArch.UseCase.Options;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Apps.UseCases;
using Pedidos.Domain.Produtos.Entities;

namespace Pedidos.Apps.Produtos.UseCases;

public class ListarTodosProdutosUseCase : UseCase<ListarTodosProdutosUseCase, Empty<object>, ICollection<Produto>>
{
    private readonly IProdutoGateway _produtoGateway;

    public ListarTodosProdutosUseCase(ILogger<ListarTodosProdutosUseCase> logger, IProdutoGateway produtoGateway) :
        base(logger)
    {
        _produtoGateway = produtoGateway;
    }

    protected override async Task<ICollection<Produto>?> Execute(Empty<object> empty)
    {
        return await _produtoGateway.ListarTodosProdutosAsync();
    }
}
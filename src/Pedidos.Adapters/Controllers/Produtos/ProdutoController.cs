using Microsoft.Extensions.Logging;
using Pedidos.Adapters.Presenters.Produtos;
using Pedidos.Apps.Produtos.Enums;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Apps.Produtos.UseCases;
using Pedidos.Apps.Produtos.UseCases.DTOs;
using Pedidos.Apps.Types.Results;
using Pedidos.Apps.UseCases;
using Pedidos.Domain.Produtos.Entities;

namespace Pedidos.Adapters.Controllers.Produtos;

public class ProdutoController : IProdutoController
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IProdutoGateway _produtoGateway;

    public ProdutoController(ILoggerFactory loggerFactory, IProdutoGateway produtoGateway)
    {
        _loggerFactory = loggerFactory;
        _produtoGateway = produtoGateway;
    }

    public async Task<Result<ProdutoCriadoDto>> CreateProdutoAsync(NovoProdutoDto novoProduto)
    {
        var useCase = new CriarProdutoUseCase(_loggerFactory.CreateLogger<CriarProdutoUseCase>(), _produtoGateway);
        var useCaseResult = await useCase.ResolveAsync(novoProduto);

        return ControllerResultBuilder<ProdutoCriadoDto, Produto>
            .ForUseCase(useCase)
            .WithInstance(novoProduto.Nome)
            .WithResult(useCaseResult)
            .AdaptUsing(ProdutoPresenter.AdaptProdutoCriado)
            .Build();
    }

    public async Task<Result<ICollection<ProdutoDto>>> ListarProdutoPorCategoriaAsync(ProdutoCategoria categoria)
    {
        var useCase =
            new ListarProdutoPorCategoriaUseCase(_loggerFactory.CreateLogger<ListarProdutoPorCategoriaUseCase>(),
                _produtoGateway);
        var useCaseResult = await useCase.ResolveAsync(categoria);

        return ControllerResultBuilder<ICollection<ProdutoDto>, ICollection<Produto>>
            .ForUseCase(useCase)
            .WithInstance(categoria.ToString())
            .WithResult(useCaseResult)
            .AdaptUsing(ProdutoPresenter.AdaptProduto)
            .Build();
    }

    public async Task<Result<ICollection<ProdutoDto>>> GetAllProdutosAsync()
    {
        var useCase = new ListarTodosProdutosUseCase(_loggerFactory.CreateLogger<ListarTodosProdutosUseCase>(),
            _produtoGateway);
        var useCaseResult = await useCase.ResolveAsync();

        return ControllerResultBuilder<ICollection<ProdutoDto>, ICollection<Produto>>
            .ForUseCase(useCase)
            .WithInstance("produtos")
            .WithResult(useCaseResult)
            .AdaptUsing(ProdutoPresenter.AdaptProduto)
            .Build();
    }

    public async Task<Result<ProdutoDto?>> GetProdutoByIdAsync(Guid id)
    {
        var useCase = new EncontrarProdutoPorIdUseCase(_loggerFactory.CreateLogger<EncontrarProdutoPorIdUseCase>(),
            _produtoGateway);
        var useCaseResult = await useCase.ResolveAsync(id);

        return ControllerResultBuilder<ProdutoDto?, Produto>
            .ForUseCase(useCase)
            .WithInstance(id)
            .WithResult(useCaseResult)
            .AdaptUsing(ProdutoPresenter.AdaptProduto)
            .Build();
    }
}
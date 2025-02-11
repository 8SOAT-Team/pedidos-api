using CleanArch.UseCase.Options;
using Microsoft.Extensions.Logging;
using Pedidos.Adapters.Controllers.Pedidos.Dtos;
using Pedidos.Adapters.Presenters.Pedidos;
using Pedidos.Adapters.Types.Results;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Pedidos.UseCases;
using Pedidos.Apps.Pedidos.UseCases.Dtos;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Pedidos.Enums;
using NovoPedidoDto = Pedidos.Adapters.Controllers.Pedidos.Dtos.NovoPedidoDto;
using NovoPedido = Pedidos.Apps.Pedidos.UseCases.Dtos.NovoPedidoDto;
using ItemDoPedido = Pedidos.Apps.Pedidos.UseCases.Dtos.ItemDoPedidoDto;

namespace Pedidos.Adapters.Controllers.Pedidos;

public class PedidoController(
    ILoggerFactory logger,
    IPedidoGateway pedidoGateway,
    IProdutoGateway produtoGateway,
    IPedidoHandler pedidoHandler) : IPedidoController
{
    private async Task PedidosHandleEvents(Pedido pedido)
    {
        foreach (var @event in pedido.ReleaseEvents())
        {
            await pedidoHandler.HandleAsync(@event!);
        }
    }
    
    public async Task<Result<PedidoDto>> AtualizarStatusDePreparacaoDoPedido(StatusPedido novoStatus, Guid pedidoId)
    {
        var useCase =
            new AtualizarStatusDePreparoPedidoUseCase(logger.CreateLogger<AtualizarStatusDePreparoPedidoUseCase>(),
                pedidoGateway);
        var useCaseResult = await useCase.ResolveAsync(new NovoStatusDePedidoDto
        {
            NovoStatus = novoStatus,
            PedidoId = pedidoId
        });

        return ControllerResultBuilder<PedidoDto, Pedido>
            .ForUseCase(useCase)
            .WithInstance(pedidoId)
            .WithResult(useCaseResult)
            .AdaptUsing(PedidoPresenter.ToPedidoDto)
            .Build();
    }

    public async Task<Result<PedidoDto>> CreatePedidoAsync(NovoPedidoDto pedido)
    {
        var useCase = new CriarNovoPedidoUseCase(logger.CreateLogger<CriarNovoPedidoUseCase>(), pedidoGateway,
            produtoGateway);
        var useCaseResult = await useCase.ResolveAsync(new NovoPedido
        {
            ClienteId = pedido.ClienteId,
            ItensDoPedido = pedido.ItensDoPedido.Select(i => new ItemDoPedido
            {
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade
            }).ToList()
        });

        var pedidoCriado = useCaseResult.Value;
        
        pedidoCriado.IniciarPagamento(MetodoDePagamento.Cartao);
        
        await PedidosHandleEvents(pedidoCriado);
        
        return ControllerResultBuilder<PedidoDto, Pedido>
            .ForUseCase(useCase)
            .WithResult(useCaseResult)
            .AdaptUsing(PedidoPresenter.ToPedidoDto)
            .Build();
    }

    public async Task<Result<List<PedidoDto>>> GetAllPedidosAsync()
    {
        var useCase = new ListarTodosPedidosUseCase(logger.CreateLogger<ListarTodosPedidosUseCase>(), pedidoGateway);
        var useCaseResult = await useCase.ResolveAsync(Any<object>.Empty);

        return ControllerResultBuilder<List<PedidoDto>, List<Pedido>>
            .ForUseCase(useCase)
            .WithResult(useCaseResult)
            .AdaptUsing(PedidoPresenter.ToListPedidoDto)
            .Build();
    }

    public async Task<Result<List<PedidoDto>>> GetAllPedidosPending()
    {
        var useCase = new ObterListaPedidosPendentesUseCase(logger.CreateLogger<ObterListaPedidosPendentesUseCase>(),
            pedidoGateway);
        var useCaseResult = await useCase.ResolveAsync(Any<object>.Empty);

        return ControllerResultBuilder<List<PedidoDto>, List<Pedido>>
            .ForUseCase(useCase)
            .WithResult(useCaseResult)
            .AdaptUsing(PedidoPresenter.ToListPedidoDto)
            .Build();
    }

    public async Task<Result<PedidoDto>> GetPedidoByIdAsync(Guid id)
    {
        var useCase =
            new EncontrarPedidoPorIdUseCase(logger.CreateLogger<EncontrarPedidoPorIdUseCase>(), pedidoGateway);
        var useCaseResult = await useCase.ResolveAsync(id);

        return ControllerResultBuilder<PedidoDto, Pedido>
            .ForUseCase(useCase)
            .WithResult(useCaseResult)
            .AdaptUsing(PedidoPresenter.ToPedidoDto)
            .Build();
    }
}
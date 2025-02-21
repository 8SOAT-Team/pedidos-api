using System.Diagnostics.CodeAnalysis;
using CleanArch.UseCase.Faults;
using Pedidos.Apps.Pedidos.EventHandlers;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Pedidos.UseCases.Dtos;
using Pedidos.Apps.Types.Results;
using Pedidos.Apps.UseCases;
using Pedidos.Domain.Pedidos.Entities;

namespace Pedidos.Apps.Pedidos.UseCases;
[ExcludeFromCodeCoverage]
public sealed class AtualizarPagamentoPedidoUseCase(
    ILogger<AtualizarPagamentoPedidoUseCase> logger,
    IPedidoGateway pedidoGateway,
    IPedidoHandler pedidoHandler)
    : UseCase<AtualizarPagamentoPedidoUseCase, AtualizarStatusPagamentoDto, Pedido>(logger)
{
    protected override async Task<Pedido?> Execute(AtualizarStatusPagamentoDto command)
    {
        var pedido = await pedidoGateway.GetByIdAsync(command.PedidoId);
        
        if (pedido is null)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Pedido não encontrado"));
            return null;
        }
        
        pedido.IniciarPreparo(command.StatusPagamento);
        
        var problems = new List<AppProblemDetails>();
        foreach (var @event in pedido.ReleaseEvents())
        {
            var result = await pedidoHandler.HandleAsync(@event!);
            result.Match(p => pedido = p, p => problems.AddRange(p));
        }
        
        if (problems.Count != 0)
        {
            AddError(problems.Select(p => new UseCaseError(UseCaseErrorType.InternalError, p.Detail)));
            return pedido;
        }

        var pedidoAtualizado = await pedidoGateway.UpdateAsync(pedido);

        return pedidoAtualizado;
    }
}
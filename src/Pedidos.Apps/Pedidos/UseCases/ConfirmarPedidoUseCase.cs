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
public sealed class ConfirmarPedidoUseCase(
    ILogger<ConfirmarPedidoUseCase> logger,
    IPedidoGateway pedidoGateway,
    IPedidoHandler pedidoHandler)
    : UseCase<ConfirmarPedidoUseCase, ConfirmarPedidoDto, Pedido>(logger)
{
    protected override async Task<Pedido?> Execute(ConfirmarPedidoDto command)
    {
        var pedido = await pedidoGateway.GetByIdAsync(command.PedidoId);

        if (pedido is null)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Pedido não encontrado"));
            return null;
        }

        pedido.ConfirmarPedido(command.MetodoDePagamento);

        var problems = new List<AppProblemDetails>();
        var events = pedido.ReleaseEvents().ToList();
        foreach (var @event in events)
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
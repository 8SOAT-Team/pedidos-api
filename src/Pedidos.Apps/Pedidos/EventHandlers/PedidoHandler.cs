using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Apps.Types.Results;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Pedidos.DomainEvents;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Apps.Pedidos.EventHandlers;

public interface IPedidoHandler
{
    Task<Result<Pedido>> HandleAsync(DomainEvent domainEvent);
}

public record PedidoRealizadoDomainEventResponse : DomainEventResponse
{
    public Guid Id { get; init; }
    public Guid PagamentoId { get; init; }
    public StatusPagamento Status { get; init; }
    public string? UrlPagamento { get; init; }
}

public class PedidoHandler(IPedidoGateway pedidoGateway) : IPedidoHandler
{
    public async Task<Result<Pedido>> HandleAsync(PedidoConfirmado domainEvent)
    {
        var pedido = await pedidoGateway.IniciarPagamentoAsync(new NovoPagamentoDto
        {
            PedidoId = domainEvent.PedidoId,
            MetodoDePagamento = (MetodosDePagamento)domainEvent.MetodoDePagamento,
            Itens = domainEvent.Itens.Select(x => new NovoPagamentoItemRequest
            {
                Id = x.Id,
                Titulo = x.Produto.Nome,
                Descricao = x.Produto.Descricao,
                Quantidade = x.Quantidade,
                PrecoUnitario = x.Produto.Preco,
            }).ToList(),
            Pagador = domainEvent.Cliente is not null
                ? new NovoPagamentoPagadorRequest
                {
                    Email = domainEvent.Cliente.Email,
                    Nome = domainEvent.Cliente.Nome,
                    Cpf = domainEvent.Cliente.Cpf
                }
                : null
        });

        return Result<Pedido>.Succeed(pedido);
    }

    public async Task<Result<Pedido>> HandleAsync(DomainEvent domainEvent)
    {
        var domainEventAction = domainEvent switch
        {
            PedidoConfirmado pedidoConfirmado => HandleAsync(pedidoConfirmado),
            _ => throw new Exception($"Evento não suportado: {domainEvent.GetType()}")
        };

        var domainEventResult = await domainEventAction;

        return domainEventResult.IsSucceed
            ? Result<Pedido>.Succeed(domainEventResult.Value!)
            : Result<Pedido>.Failure(domainEventResult.ProblemDetails);
    }
}
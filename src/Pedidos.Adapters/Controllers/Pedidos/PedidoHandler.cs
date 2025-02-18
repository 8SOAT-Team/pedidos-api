using Pedidos.Adapters.Controllers.Pedidos.Enums;
using Pedidos.Adapters.Gateways.Pagamentos;
using Pedidos.Adapters.Gateways.Pagamentos.Dtos;
using Pedidos.Adapters.Types.Results;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Pedidos.DomainEvents;

namespace Pedidos.Adapters.Controllers.Pedidos;

public interface IPedidoHandler
{
    Task<Result<PagamentoCriadoDto>> HandleAsync(PedidoRealizado domainEvent);
    Task<Result<PagamentoCriadoDto>> HandleAsync(DomainEvent domainEvent);
    
}

public class PedidoHandler(IPagamentoGateway pagamentoGateway) : IPedidoHandler
{
    public async Task<Result<PagamentoCriadoDto>> HandleAsync(PedidoRealizado domainEvent)
    {
        var pagamento = await pagamentoGateway.IniciarPagamentoAsync(new NovoPagamentoDto
        {
            PedidoId = domainEvent.PedidoId,
            ValorTotal = domainEvent.ValorTotal,
            EmailPagador = domainEvent.Cliente?.Email.Address
        });
    
        return pagamento.IsSucceed ? Result<PagamentoCriadoDto>.Succeed(new PagamentoCriadoDto
        {
            Id = pagamento.Value.Id,
            Status = (StatusDoPagamento)pagamento.Value.Status,
            UrlPagamento = pagamento.Value.UrlPagamento,
        }) : Result<PagamentoCriadoDto>.Failure(pagamento.ProblemDetails); 
    }

    public Task<Result<PagamentoCriadoDto>> HandleAsync(DomainEvent domainEvent) => domainEvent switch
    {
        PedidoRealizado pedidoRealizado => HandleAsync(pedidoRealizado),
        _ => throw new Exception($"Evento não suportado: {domainEvent.GetType()}" )
    };
   
}
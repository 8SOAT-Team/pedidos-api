using Pedidos.Adapters.Controllers.Pedidos.Enums;

namespace Pedidos.Adapters.Gateways.Pagamentos.Dtos;

public record PagamentoCriadoDto
{
    public Guid Id { get; init; }
    public StatusDoPagamento Status { get; init; }
    public string? UrlPagamento { get; init; }
}
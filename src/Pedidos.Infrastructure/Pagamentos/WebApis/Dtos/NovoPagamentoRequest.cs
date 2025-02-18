using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Pedidos.Adapters.Controllers.Pedidos.Enums;
using Pedidos.Adapters.Gateways.Pagamentos.Enums;

namespace Pedidos.Infrastructure.Pagamentos.WebApis.Dtos;

public record NovoPagamentoRequest
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MetodosDePagamento MetodoDePagamento { get; init; }
    public string? EmailPagador { get; init; }
    public decimal ValorTotal { get; init; }
}

public record PagamentoResponse(
    Guid Id,
    MetodosDePagamento MetodoDePagamento,
    StatusDoPagamento Status,
    decimal ValorTotal,
    string PagamentoExternoId,
    string? UrlPagamento);
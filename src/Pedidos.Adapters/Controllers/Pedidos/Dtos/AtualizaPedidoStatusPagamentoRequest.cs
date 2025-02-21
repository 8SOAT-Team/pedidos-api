using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Adapters.Controllers.Pedidos.Dtos;

[ExcludeFromCodeCoverage]
public record AtualizaPedidoStatusPagamentoRequest
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusPagamento Status { get; init; }
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Pedidos.Adapters.Controllers.Pedidos.Enums;
using Pedidos.Adapters.Gateways.Pagamentos.Enums;

namespace Pedidos.Infrastructure.Pagamentos.WebApis.Dtos;

public record NovoPagamentoRequest
{
    public Guid PedidoId { get; init; }
    
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MetodosDePagamento MetodoDePagamento { get; init; }

    public List<NovoPagamentoItemRequest> Itens { get; init; } = [];

    public NovoPagamentoPagadorRequest? Pagador { get; init; }
}
public record NovoPagamentoItemRequest
{
    public Guid Id { get; init; }
    public string Titulo { get; init; }
    public string Descricao { get; init; }
    public int Quantidade { get; init; }
    public decimal PrecoUnitario { get; init; }
}

public record NovoPagamentoPagadorRequest
{
    public string Email { get; init; }
    public string Nome { get; init; }
    public string Cpf { get; init; }
}

public record PagamentoResponse(
    Guid Id,
    MetodosDePagamento MetodoDePagamento,
    StatusDoPagamento Status,
    decimal ValorTotal,
    string PagamentoExternoId,
    string? UrlPagamento);
    

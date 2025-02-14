namespace Pedidos.Apps.Pedidos.Gateways;

public record NovoPagamentoDto
{
    public Guid PedidoId { get; init; }
    public string? EmailPagador { get; init; }
    public decimal ValorTotal { get; init; }
};
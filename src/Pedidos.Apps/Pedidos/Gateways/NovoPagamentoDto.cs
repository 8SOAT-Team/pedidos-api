namespace Pedidos.Apps.Pedidos.Gateways;

public enum MetodosDePagamento
{
    Pix = 0,
}

public record NovoPagamentoDto
{
    public Guid PedidoId { get; init; }
    public MetodosDePagamento MetodoDePagamento { get; init; }

    public List<NovoPagamentoItemRequest> Itens { get; init; } = [];

    public NovoPagamentoPagadorRequest? Pagador { get; init; }
};

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
namespace Pedidos.Adapters.Gateways.Pagamentos.Enums;

public enum MetodosDePagamento
{
    Pix = 1 << 1,
    Cartao = 1 << 2,
    Master = 1 << 3,
    Visa = 1 << 4,
}
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Domain.Pedidos.ValueObjects;

public class Pagamento
{
    public Guid Id { get; init; }
    public string? IdExterno { get; private set; }
    public StatusPagamento Status { get; init; }

    public bool EstaAutorizado() => Status == StatusPagamento.Autorizado;

    public void AssociarIdExterno(string idExterno)
    {
        IdExterno = idExterno;
    }
}
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Domain.Pedidos.ValueObjects;

public class Pagamento
{
    public Guid? Id { get; init; }
    public StatusPagamento? Status { get; set; }
    public string? UrlPagamento { get; init; }
    
    public bool EstaAutorizado()
    {
        return Status == StatusPagamento.Autorizado;
    }
    
    public void AtualizarStatus(StatusPagamento status)
    {
        Status = status;
    }
}
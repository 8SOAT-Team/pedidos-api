namespace Pedidos.Domain.Entities;

public abstract record DomainEvent
{
    public Guid EventId { get; protected init; } = Guid.NewGuid();
    public DateTime Timestamp { get; protected init; } = DateTime.UtcNow;
};
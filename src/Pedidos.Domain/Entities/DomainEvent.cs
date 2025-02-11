using System.Diagnostics.CodeAnalysis;

namespace Pedidos.Domain.Entities;

[ExcludeFromCodeCoverage]
public abstract record DomainEvent
{
    public Guid EventId { get; protected init; } = Guid.NewGuid();
    public DateTime Timestamp { get; protected init; } = DateTime.UtcNow;
}
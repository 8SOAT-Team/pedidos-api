using System.Collections.Concurrent;

namespace Pedidos.Domain.Entities;

public abstract class Entity : IEntity
{
    private readonly ConcurrentQueue<DomainEvent> _domainEvents = [];

    public Guid Id { get; protected init; }

    protected void RaiseEvent(DomainEvent domainEvent)
    {
        _domainEvents.Enqueue(domainEvent);
    }

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }

    public IEnumerable<DomainEvent?> ReleaseEvents()
    {
        while (_domainEvents.TryDequeue(out var domainEvent))
        {
            yield return domainEvent;
        }
    }
}
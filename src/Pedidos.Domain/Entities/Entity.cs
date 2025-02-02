﻿using System.Collections.Concurrent;

namespace Pedidos.Domain.Entities;
public abstract class Entity : IEntity
{
    private ConcurrentQueue<DomainEvent> _domainEvents = [];

    public Guid Id { get; protected init; }
    
    protected void RaiseEvent(DomainEvent domainEvent) => _domainEvents.Enqueue(domainEvent);
    
    public void ClearEvents()
    {
        _domainEvents.Clear();
    }

    public IEnumerable<DomainEvent?> ReleaseEvents()
    {
        yield return _domainEvents.TryDequeue(out var domainEvent) ? domainEvent : null;
    }
}
using Pedidos.Domain.Entities;

namespace Pedidos.Tests.UnitTests.Domain.Entities;

public class EntityTest
{
    private class FakeEntity : Entity
    {
        public FakeEntity(Guid id)
        {
            Id = id;
        }

        public new void RaiseEvent(DomainEvent domainEvent) => base.RaiseEvent(domainEvent);
    }

    private record FakeDomainEvent : DomainEvent;

    [Fact]
    public void Entity_WhenRaiseEvent_ShouldAddEventToQueue()
    {
        // Arrange
        var entity = new FakeEntity(Guid.NewGuid());
        var domainEvent = new FakeDomainEvent();

        // Act
        entity.RaiseEvent(domainEvent);

        // Assert
        Assert.Single(entity.ReleaseEvents());
    }

    [Fact]
    public void Entity_WhenClearEvents_ShouldClearEventsQueue()
    {
        // Arrange
        var entity = new FakeEntity(Guid.NewGuid());
        var domainEvent = new FakeDomainEvent();

        // Act
        entity.RaiseEvent(domainEvent);
        entity.ClearEvents();

        // Assert
        Assert.Empty(entity.ReleaseEvents());
    }

    [Fact]
    public void Entity_WhenReleaseEvents_ShouldReturnEventsQueue()
    {
        // Arrange
        var entity = new FakeEntity(Guid.NewGuid());
        var domainEvent = new FakeDomainEvent();

        // Act
        entity.RaiseEvent(domainEvent);

        // Assert
        Assert.Single(entity.ReleaseEvents());
    }

    [Fact]
    public void Entity_WhenReleaseEvents_ShouldClearEventsQueue()
    {
        // Arrange
        var entity = new FakeEntity(Guid.NewGuid());
        var domainEvent = new FakeDomainEvent();

        // Act
        entity.RaiseEvent(domainEvent);
        _ = entity.ReleaseEvents().ToArray();

        // Assert
        Assert.Empty(entity.ReleaseEvents());
    }
}
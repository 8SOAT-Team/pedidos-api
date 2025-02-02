using Pedidos.Domain.Entities;

namespace Pedidos.Domain.Pedidos.DomainEvents;

public record PedidoFinalizado(Guid PedidoId) : DomainEvent;
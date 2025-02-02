using Pedidos.Domain.Entities;

namespace Pedidos.Domain.Pedidos.DomainEvents;

public record PedidoPronto(Guid PedidoId) : DomainEvent;
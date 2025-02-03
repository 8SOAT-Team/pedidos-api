using Pedidos.Domain.Entities;

namespace Pedidos.Domain.Pedidos.DomainEvents;

public record PedidoCancelado(Guid PedidoId) : DomainEvent;
using Pedidos.Domain.Entities;

namespace Pedidos.Domain.Pedidos.DomainEvents;

public record PedidoEmPreparacao(Guid PedidoId) : DomainEvent;
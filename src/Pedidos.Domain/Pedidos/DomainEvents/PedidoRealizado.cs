using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.Entities;

namespace Pedidos.Domain.Pedidos.DomainEvents;

public record PedidoRealizado(Guid PedidoId, decimal ValorTotal, Cliente? Cliente)
    : DomainEvent;
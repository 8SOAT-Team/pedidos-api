using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Domain.Pedidos.DomainEvents;

public record PedidoRealizado(Guid PedidoId, decimal ValorTotal, MetodoDePagamento MetodoDePagamento, Cliente? Cliente)
    : DomainEvent;
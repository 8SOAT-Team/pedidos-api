using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Domain.Pedidos.DomainEvents;

public record PedidoConfirmado(
    Guid PedidoId,
    MetodoDePagamento MetodoDePagamento,
    Cliente? Cliente,
    List<ItemDoPedido> Itens) : DomainEvent;
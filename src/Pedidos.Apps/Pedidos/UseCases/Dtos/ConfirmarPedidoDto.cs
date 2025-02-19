using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Apps.Pedidos.UseCases.Dtos;

public record ConfirmarPedidoDto(Guid PedidoId, MetodoDePagamento MetodoDePagamento);
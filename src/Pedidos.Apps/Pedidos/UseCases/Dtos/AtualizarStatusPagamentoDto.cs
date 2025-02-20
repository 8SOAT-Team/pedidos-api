using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Apps.Pedidos.UseCases.Dtos;

public record AtualizarStatusPagamentoDto(Guid PedidoId, StatusPagamento StatusPagamento);
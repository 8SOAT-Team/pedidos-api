using Pedidos.Adapters.Controllers.Pedidos.Enums;

namespace Pedidos.Adapters.Controllers.Pedidos.Dtos;

public record PedidoConfirmadoDto(Guid PedidoId, string UrlPagamento, StatusDoPagamento Status);
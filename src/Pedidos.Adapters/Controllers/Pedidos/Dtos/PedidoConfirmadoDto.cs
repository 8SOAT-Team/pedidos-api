using System.Diagnostics.CodeAnalysis;
using Pedidos.Adapters.Controllers.Pedidos.Enums;

namespace Pedidos.Adapters.Controllers.Pedidos.Dtos;
[ExcludeFromCodeCoverage]
public record PedidoConfirmadoDto(Guid PedidoId, string UrlPagamento, StatusDoPagamento Status);
using System.Diagnostics.CodeAnalysis;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Adapters.Controllers.Pedidos.Dtos;
[ExcludeFromCodeCoverage]
public record AtualizaPedidoStatusPagamentoRequest(StatusPagamento Status);
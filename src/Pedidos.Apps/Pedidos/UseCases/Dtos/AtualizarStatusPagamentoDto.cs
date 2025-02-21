using System.Diagnostics.CodeAnalysis;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Apps.Pedidos.UseCases.Dtos;
[ExcludeFromCodeCoverage]
public record AtualizarStatusPagamentoDto(Guid PedidoId, StatusPagamento StatusPagamento);
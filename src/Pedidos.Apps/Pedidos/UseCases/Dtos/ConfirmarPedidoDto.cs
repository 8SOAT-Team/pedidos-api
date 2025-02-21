using System.Diagnostics.CodeAnalysis;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Apps.Pedidos.UseCases.Dtos;
[ExcludeFromCodeCoverage]

public record ConfirmarPedidoDto(Guid PedidoId, MetodoDePagamento MetodoDePagamento);
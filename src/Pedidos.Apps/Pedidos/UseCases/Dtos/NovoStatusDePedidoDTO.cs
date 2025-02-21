using System.Diagnostics.CodeAnalysis;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Apps.Pedidos.UseCases.Dtos;
[ExcludeFromCodeCoverage]
public record NovoStatusDePedidoDto
{
    public Guid PedidoId { get; init; }
    public StatusPedido NovoStatus { get; init; }
}
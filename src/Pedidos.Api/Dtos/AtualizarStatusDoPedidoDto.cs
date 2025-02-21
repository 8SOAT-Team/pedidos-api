using System.Diagnostics.CodeAnalysis;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Api.Dtos;
[ExcludeFromCodeCoverage]
public record AtualizarStatusDoPedidoDto
{
    public StatusPedido NovoStatus { get; init; }
}
using System.Diagnostics.CodeAnalysis;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Api.Dtos;
[ExcludeFromCodeCoverage]
public record PedidoMetodoDePagamentoDto(MetodoDePagamento MetodoDePagamento);
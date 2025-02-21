using System.Diagnostics.CodeAnalysis;
using Pedidos.Adapters.Controllers.Pedidos.Enums;

namespace Pedidos.Api.Dtos;
[ExcludeFromCodeCoverage]
public record ConfirmarPagamentoDto(StatusDoPagamento Status);
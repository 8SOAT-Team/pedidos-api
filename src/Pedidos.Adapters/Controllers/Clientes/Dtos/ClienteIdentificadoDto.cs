using System.Diagnostics.CodeAnalysis;

namespace Pedidos.Adapters.Controllers.Clientes.Dtos;
[ExcludeFromCodeCoverage]
public record ClienteIdentificadoDto(Guid Id, string Nome);
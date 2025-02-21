using System.Diagnostics.CodeAnalysis;

namespace Pedidos.Adapters.Controllers.Clientes.Dtos;
[ExcludeFromCodeCoverage]
public record NovoClienteDto(string Cpf, string Nome, string Email);
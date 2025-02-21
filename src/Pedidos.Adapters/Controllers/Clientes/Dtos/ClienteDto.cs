using System.Diagnostics.CodeAnalysis;

namespace Pedidos.Adapters.Controllers.Clientes.Dtos;
[ExcludeFromCodeCoverage]
public record ClienteDto
{
    public Guid Id { get; init; }
    public string Nome { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Cpf { get; init; } = null!;
}
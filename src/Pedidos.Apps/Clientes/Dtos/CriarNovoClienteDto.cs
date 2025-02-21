using System.Diagnostics.CodeAnalysis;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Apps.Clientes.Dtos;
[ExcludeFromCodeCoverage]
public record CriarNovoClienteDto(Cpf Cpf, string Nome, EmailAddress Email);
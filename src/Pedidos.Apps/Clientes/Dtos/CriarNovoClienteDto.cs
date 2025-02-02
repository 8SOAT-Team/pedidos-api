using Pedidos.Domain.ValueObjects;

namespace Pedidos.Apps.Clientes.Dtos;

public record CriarNovoClienteDto(Cpf Cpf, string Nome, EmailAddress Email);

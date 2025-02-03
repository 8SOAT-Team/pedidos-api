using Pedidos.Adapters.Controllers.Clientes.Dtos;
using Pedidos.Domain.Clientes.Entities;

namespace Pedidos.Adapters.Presenters.Clientes;

public static class ClientePresenter
{
    public static ClienteIdentificadoDto AdaptClienteIdentificado(Cliente cliente)
    {
        return new ClienteIdentificadoDto(cliente.Id, cliente.Nome);
    }

    public static ClienteDto AdaptCliente(Cliente cliente)
    {
        return new ClienteDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Email = cliente.Email,
            Cpf = cliente.Cpf
        };
    }
}
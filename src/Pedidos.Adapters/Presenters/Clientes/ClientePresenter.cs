using Pedidos.Adapters.Controllers.Clientes.Dtos;
using Pedidos.Domain.Clientes.Entities;

namespace Pedidos.Adapters.Presenters.Clientes;

public static class ClientePresenter
{
    public static ClienteIdentificadoDto AdaptClienteIdentificado(Cliente cliente) => new(cliente.Id, cliente.Nome);

    public static ClienteDto AdaptCliente(Cliente cliente) => new()
    {
        Id = cliente.Id,
        Nome = cliente.Nome,
        Email = cliente.Email,
        Cpf = cliente.Cpf,
    };
}


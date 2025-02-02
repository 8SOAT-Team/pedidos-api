using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Apps.Clientes.Gateways;
public interface IClienteGateway
{
    Task<Cliente?> GetClienteByCpfAsync(Cpf cpf);
    Task<Cliente> InsertCliente(Cliente cliente);
}

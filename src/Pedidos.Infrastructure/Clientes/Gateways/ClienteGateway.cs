using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pedidos.Apps.Clientes.Gateways;
using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.ValueObjects;
using Pedidos.Infrastructure.Databases;

namespace Pedidos.Infrastructure.Clientes.Gateways;

public class ClienteGateway : IClienteGateway
{
    private readonly DbSet<Cliente> _clientes;
    private readonly FastOrderContext _context;

    public ClienteGateway(FastOrderContext context)
    {
        _context = context;
        _clientes = _context.Set<Cliente>();
    }

    public Task<Cliente?> GetClienteByCpfAsync(Cpf cpf)
    {
        const string query = "SELECT * FROM Clientes WHERE cpf = @cpf";
        return _clientes.FromSqlRaw(query, new SqlParameter("cpf", cpf.GetSanitized()))
            .FirstOrDefaultAsync();
    }

    public async Task<Cliente> InsertCliente(Cliente cliente)
    {
        var insertedCliente = await _clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();

        return insertedCliente.Entity;
    }
}
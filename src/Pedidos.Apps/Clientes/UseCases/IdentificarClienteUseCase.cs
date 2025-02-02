using Microsoft.Extensions.Logging;
using Pedidos.Apps.Clientes.Gateways;
using Pedidos.Apps.UseCases;
using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Apps.Clientes.UseCases;

public class IdentificarClienteUseCase(ILogger<IdentificarClienteUseCase> logger,
    IClienteGateway clienteGateway)
    : UseCase<IdentificarClienteUseCase, Cpf, Cliente>(logger)
{
    protected override Task<Cliente?> Execute(Cpf document)
    {
        return clienteGateway.GetClienteByCpfAsync(document);
    }
}
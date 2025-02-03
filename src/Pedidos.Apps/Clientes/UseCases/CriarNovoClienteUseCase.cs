using CleanArch.UseCase.Faults;
using Pedidos.Apps.Clientes.Dtos;
using Pedidos.Apps.Clientes.Gateways;
using Pedidos.Apps.UseCases;
using Pedidos.Domain.Clientes.Entities;

namespace Pedidos.Apps.Clientes.UseCases;

public class CriarNovoClienteUseCase(ILogger<CriarNovoClienteUseCase> logger, IClienteGateway clienteGateway)
    : UseCase<CriarNovoClienteUseCase, CriarNovoClienteDto, Cliente>(logger)
{
    protected override async Task<Cliente?> Execute(CriarNovoClienteDto novoCliente)
    {
        var existingCliente = await clienteGateway.GetClienteByCpfAsync(novoCliente.Cpf);

        if (existingCliente != null)
        {
            AddError(new UseCaseError(UseCaseErrorType.BadRequest, "Cpf já cadastrado!"));
            return null;
        }

        var cliente = new Cliente(Guid.NewGuid(), novoCliente.Cpf, novoCliente.Nome, novoCliente.Email);
        return await clienteGateway.InsertCliente(cliente);
    }
}
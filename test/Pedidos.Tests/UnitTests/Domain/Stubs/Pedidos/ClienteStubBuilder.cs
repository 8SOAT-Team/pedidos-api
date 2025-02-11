using Bogus;
using Bogus.Extensions.Brazil;
using Pedidos.Domain.Clientes.Entities;

namespace Pedidos.Tests.UnitTests.Domain.Stubs.Pedidos;

internal sealed class ClienteStubBuilder : Faker<Cliente>
{
    private ClienteStubBuilder()
    {
        CustomInstantiator(f => new Cliente(f.Person.Cpf(), f.Person.FullName, f.Person.Email));
    }

    public static Cliente Create() => new ClienteStubBuilder().Generate();
}
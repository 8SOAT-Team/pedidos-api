using Bogus;
using Bogus.Extensions.Brazil;
using Pedidos.Domain.Clientes.Entities;

namespace Pedidos.Tests.IntegrationTests.Builder; 
public class ClienteBuilder : Faker<Cliente>
{
    public ClienteBuilder()
    {
        CustomInstantiator(f => new Cliente(f.Person.Cpf(), f.Person.FullName, f.Person.Email));
    }
    public Cliente Build() => Generate();
}

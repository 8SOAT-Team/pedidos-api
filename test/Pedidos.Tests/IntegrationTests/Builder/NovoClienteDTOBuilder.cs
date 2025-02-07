using Bogus;
using Bogus.Extensions.Brazil;
using Pedidos.Adapters.Controllers.Clientes.Dtos;

namespace Pedidos.Tests.IntegrationTests.Builder;
public class NovoClienteDtoBuilder : Faker<NovoClienteDto>
{
    public NovoClienteDtoBuilder()
    {
        CustomInstantiator(f => new NovoClienteDto(f.Person.Cpf(), f.Person.FullName, f.Person.Email));
    }

    public NovoClienteDto Build() => Generate();
}

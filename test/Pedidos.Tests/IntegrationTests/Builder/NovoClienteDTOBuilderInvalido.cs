using Bogus;
using Pedidos.Adapters.Controllers.Clientes.Dtos;

namespace Pedidos.Tests.IntegrationTests.Builder;
internal class NovoClienteDtoBuilderInvalido : Faker<NovoClienteDto>
{
    public NovoClienteDtoBuilderInvalido()
    {
        CustomInstantiator(f => new NovoClienteDto("", f.Person.FullName, f.Person.Email));
    }
    public NovoClienteDto Build() => Generate();
}

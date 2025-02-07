using Bogus;
using Pedidos.Api.Dtos;
using Postech8SOAT.FastOrder.Tests.Integration.Builder;

namespace Pedidos.Tests.IntegrationTests.Builder;
public class CategoriaBuilder : Faker<CategoriaDto>
{

    public CategoriaBuilder()
    {
        CustomInstantiator(f => new CategoriaDto(RetornaIdCategoriaUtil.RetornaIdCategoria(), f.Commerce.Categories(1).ToString()!, f.Commerce.Categories(1).ToString()!));
    }
    public CategoriaDto Build() => Generate();
}
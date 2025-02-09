using Bogus;
using Pedidos.Adapters.Controllers.Pedidos.Dtos;

namespace Pedidos.Tests.IntegrationTests.Builder;
internal sealed class NovoItemDePedidoBuilder : Faker<NovoItemDePedido>
{
    public NovoItemDePedidoBuilder()
    {
        RuleFor(p => p.ProdutoId, f => f.Random.Guid());
        RuleFor(p => p.Quantidade, f => f.Random.Int(1, 10));
    }
    
    public NovoItemDePedidoBuilder WithProdutoId(Guid produtoId)
    {
        RuleFor(p => p.ProdutoId, produtoId);
        return this;
    }
    
    public static NovoItemDePedidoBuilder CreateBuilder() => new();
}

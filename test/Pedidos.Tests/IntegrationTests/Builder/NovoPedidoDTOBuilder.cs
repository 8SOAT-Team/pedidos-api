using Bogus;
using Pedidos.Adapters.Controllers.Pedidos.Dtos;

namespace Pedidos.Tests.IntegrationTests.Builder;

internal sealed class NovoPedidoDtoBuilder : Faker<NovoPedidoDto>
{
    public NovoPedidoDtoBuilder()
    {
        RuleFor(p => p.ClienteId, f => f.Random.Guid());
        RuleFor(p => p.ItensDoPedido, f => f.Make(3, () => NovoItemDePedidoBuilder.CreateBuilder().Generate()));
    }

    public NovoPedidoDtoBuilder WithClientId(Guid? clienteId)
    {
        RuleFor(p => p.ClienteId, clienteId);
        return this;
    }

    public NovoPedidoDtoBuilder WithItensDoPedido(
        Func<Faker, NovoItemDePedidoBuilder, List<NovoItemDePedido>> itensDoPedido)
    {
        RuleFor(p => p.ItensDoPedido, f => itensDoPedido(f, NovoItemDePedidoBuilder.CreateBuilder()));
        return this;
    }

    public static NovoPedidoDtoBuilder CreateBuilder() => new();

    public static NovoPedidoDto CreateValid(Func<Faker, NovoItemDePedidoBuilder, List<NovoItemDePedido>> itensDoPedido,
        Guid? clienteId = null)
        => CreateBuilder()
            .WithClientId(clienteId)
            .WithItensDoPedido(itensDoPedido)
            .Generate();

    public static NovoPedidoDto CreateInvalid() => CreateBuilder()
        .WithClientId(Guid.Empty)
        .Generate();
}
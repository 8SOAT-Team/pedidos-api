using Bogus;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Tests.UnitTests.Domain.Stubs.Pedidos;

internal sealed class PedidoStubBuilder : Faker<Pedido>
{
    public PedidoStubBuilder()
    {
        var cliente = ClienteStubBuilder.Create();
        var itensDoPedido = ItemDoPedidoStubBuilder.CreateMany(f => f.Random.Int(1, 5));
        CustomInstantiator(f => new Pedido(cliente.Id, Guid.NewGuid(), itensDoPedido));
        RuleFor(x => x.Cliente, cliente);
    }

    public PedidoStubBuilder WithStatus(StatusPedido status)
    {
        RuleFor(x => x.StatusPedido, status);
        return this;
    }

    public PedidoStubBuilder WithPagamento(StatusPagamento status, Guid? pagamentoId = null)
    {
        RuleFor(x => x.Pagamento, PagamentoStubBuilder.NewBuilder()
            .WithId(pagamentoId ?? Guid.NewGuid()).WithStatus(status).Generate());
        return this;
    }

    public static PedidoStubBuilder NewBuilder() => new();
    public static Pedido Create() => new PedidoStubBuilder().Generate();
    public static List<Pedido> CreateMany(int qty) => new PedidoStubBuilder().Generate(qty);
}
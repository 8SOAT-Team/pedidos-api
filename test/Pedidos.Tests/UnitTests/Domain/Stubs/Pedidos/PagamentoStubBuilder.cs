using Bogus;
using Pedidos.Domain.Pedidos.Enums;
using Pedidos.Domain.Pedidos.ValueObjects;

namespace Pedidos.Tests.UnitTests.Domain.Stubs.Pedidos;

internal sealed class PagamentoStubBuilder : Faker<Pagamento>
{
    private PagamentoStubBuilder()
    { 
        CustomInstantiator(f => new Pagamento
        {
            Id = Guid.NewGuid(),
            Status = f.PickRandom<StatusPagamento>(),
        });
    }

    public PagamentoStubBuilder WithStatus(StatusPagamento status)
    {
        RuleFor(x => x.Status, status);
        return this;
    }

    public static PagamentoStubBuilder NewBuilder() => new();
    public static Pagamento Create() => new PagamentoStubBuilder().Generate();
    public static List<Pagamento> CreateMany(int qty) => new PagamentoStubBuilder().Generate(qty);
    public static Pagamento Autorizado() => NewBuilder().WithStatus(StatusPagamento.Autorizado).Generate();
    public static Pagamento Rejeitado() => NewBuilder().WithStatus(StatusPagamento.Rejeitado).Generate();
    public static Pagamento Cancelado() => NewBuilder().WithStatus(StatusPagamento.Cancelado).Generate();
}
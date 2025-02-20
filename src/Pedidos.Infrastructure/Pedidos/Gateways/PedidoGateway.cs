using Microsoft.EntityFrameworkCore;
using Pedidos.Adapters.Gateways.Pagamentos;
using Pedidos.Adapters.Gateways.Producao;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Domain.Exceptions;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Infrastructure.Databases;

namespace Pedidos.Infrastructure.Pedidos.Gateways;

public class PedidoGateway(
    FastOrderContext dbContext,
    IPagamentoGateway pagamentoGateway,
    IProducaoGateway producaoGateway) : IPedidoGateway
{
    public Task<Pedido?> GetPedidoCompletoAsync(Guid id)
    {
        return dbContext.Pedidos.Include(p => p.ItensDoPedido)
            .ThenInclude(i => i.Produto)
            .Include(i => i.Cliente)
            .SingleOrDefaultAsync(i => i.Id == id);
    }

    public Task<List<Pedido>> GetAllPedidosPending()
    {
        const string query =
            "SELECT * FROM Pedidos WHERE StatusPedido IN (3, 2, 1) ORDER BY StatusPedido DESC, DataPedido ASC";
        return dbContext.Pedidos.FromSqlRaw(query).ToListAsync();
    }

    public async Task<Pedido> AtualizarPedidoPagamentoIniciadoAsync(Pedido pedido)
    {
        dbContext.Entry(pedido).State = EntityState.Modified;
        var pedidoAtualizado = await dbContext.SaveChangesAsync();
        return pedidoAtualizado > 0 ? pedido : throw new Exception("Erro ao atualizar pedido");
    }

    public async Task<Pedido> CreateAsync(Pedido pedido)
    {
        await dbContext.Set<Pedido>().AddAsync(pedido);
        await dbContext.SaveChangesAsync();
        return pedido;
    }

    public Task<List<Pedido>> GetAllAsync()
    {
        return dbContext.Pedidos.ToListAsync();
    }

    public Task<Pedido?> GetByIdAsync(Guid id)
    {
        return dbContext.Set<Pedido>().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pedido> UpdateAsync(Pedido pedido)
    {
        dbContext.Set<Pedido>().Update(pedido);
        await dbContext.SaveChangesAsync();
        return pedido;
    }

    public async Task<Pedido> IniciarPagamentoAsync(NovoPagamentoDto dto)
    {
        var pedido = await GetByIdAsync(dto.PedidoId);
        if (pedido is null)
        {
            throw new ApplicationException("Pedido não encontrado");
        }

        var pagamentoResult = await pagamentoGateway.IniciarPagamentoAsync(dto, pedido);

        return pagamentoResult.IsSucceed ? pagamentoResult.Value! : pedido;
    }

    public async Task<Pedido> IniciarProducao(Guid pedidoId)
    {
        var pedido = await GetPedidoCompletoAsync(pedidoId);
        var response = await producaoGateway.IniciarProducaoAsync(pedido!);

        DomainExceptionValidation.When(response.IsSuccessStatusCode is false,
            $"Erro ao iniciar produção: {response.StatusCode}");

        return pedido!;
    }
}
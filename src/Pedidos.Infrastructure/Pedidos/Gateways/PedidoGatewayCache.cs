using Pedidos.Adapters.Gateways.Caches;
using Pedidos.Apps.Pedidos.Gateways;
using Pedidos.Domain.Pedidos.Entities;

namespace Pedidos.Infrastructure.Pedidos.Gateways;

public record PedidoKey(Guid? Id = null);

public class PedidoGatewayCache(IPedidoGateway nextExecution, ICacheContext cache)
    : CacheGateway<PedidoKey>(cache), IPedidoGateway
{
    private readonly ICacheContext _cache = cache;

    protected override Dictionary<string, Func<PedidoKey, (string cacheKey, bool InvalidateCacheOnChanges)>>
        CacheKeys => new()
    {
        [nameof(GetAllAsync)] = _ => ($"{nameof(PedidoGatewayCache)}:{nameof(GetAllAsync)}", true),
        [nameof(GetAllPedidosPending)] = _ => ($"{nameof(PedidoGatewayCache)}:{nameof(GetAllPedidosPending)}", true),
        [nameof(GetByIdAsync)] = p => ($"{nameof(PedidoGatewayCache)}:{nameof(GetByIdAsync)}:{p.Id}", true),
        [nameof(GetPedidoCompletoAsync)] =
            p => ($"{nameof(PedidoGatewayCache)}:{nameof(GetPedidoCompletoAsync)}:{p.Id}", false),
        [nameof(CreateAsync)] = p => ($"{nameof(Pedido)}:{p.Id}", false),
        [nameof(UpdateAsync)] = p => ($"{nameof(Pedido)}:{p.Id}", false)
    };


    public async Task<Pedido> AtualizarPedidoPagamentoIniciadoAsync(Pedido pedido)
    {
        var pedidoAtualizado = await nextExecution.AtualizarPedidoPagamentoIniciadoAsync(pedido);

        var pedidoKey = new PedidoKey(pedido.Id);
        await InvalidateCacheOnChange(pedidoKey);

        return pedidoAtualizado;
    }

    public async Task<Pedido> CreateAsync(Pedido pedido)
    {
        var pedidoCriado = await nextExecution.CreateAsync(pedido);

        var pedidoKey = new PedidoKey(pedidoCriado.Id);
        var getKey = CacheKeys[nameof(CreateAsync)];
        var (cachekey, _) = getKey(pedidoKey);
        await _cache.SetNotNullStringByKeyAsync(cachekey, pedidoCriado);

        return pedidoCriado;
    }

    public async Task<Pedido> UpdateAsync(Pedido pedido)
    {
        var pedidoAtualizado = await nextExecution.UpdateAsync(pedido);

        var pedidoKey = new PedidoKey(pedidoAtualizado.Id);
        var getKey = CacheKeys[nameof(UpdateAsync)];
        var (cacheKey, _) = getKey(pedidoKey);

        await InvalidateCacheOnChange(pedidoKey);
        await _cache.SetNotNullStringByKeyAsync(cacheKey, pedidoAtualizado);

        return pedidoAtualizado;
    }

    public async Task<List<Pedido>> GetAllAsync()
    {
        var pedidoKey = new PedidoKey();
        var getKey = CacheKeys[nameof(GetAllAsync)];
        var (cacheKey, _) = getKey(pedidoKey);

        var result = await _cache.GetItemByKeyAsync<List<Pedido>>(cacheKey);

        if (result.HasValue) return result.Value!;

        var item = await nextExecution.GetAllAsync();
        _ = await _cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }

    public async Task<List<Pedido>> GetAllPedidosPending()
    {
        var pedidoKey = new PedidoKey();
        var getKey = CacheKeys[nameof(GetAllPedidosPending)];
        var (cacheKey, _) = getKey(pedidoKey);

        var result = await _cache.GetItemByKeyAsync<List<Pedido>>(cacheKey);

        if (result.HasValue) return result.Value!;

        var item = await nextExecution.GetAllPedidosPending();
        _ = await _cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }

    public async Task<Pedido?> GetByIdAsync(Guid id)
    {
        var pedidoKey = new PedidoKey(id);
        var getKey = CacheKeys[nameof(GetByIdAsync)];
        var (cacheKey, _) = getKey(pedidoKey);
        var result = await _cache.GetItemByKeyAsync<Pedido>(cacheKey);

        if (result.HasValue) return result.Value;

        var item = await nextExecution.GetByIdAsync(id);
        _ = await _cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }

    public async Task<Pedido?> GetPedidoCompletoAsync(Guid id)
    {
        var pedidoKey = new PedidoKey(id);
        var getKey = CacheKeys[nameof(GetByIdAsync)];
        var (cacheKey, _) = getKey(pedidoKey);
        var result = await _cache.GetItemByKeyAsync<Pedido>(cacheKey);

        if (result.HasValue) return result.Value;

        var item = await nextExecution.GetPedidoCompletoAsync(id);
        _ = await _cache.SetNotNullStringByKeyAsync(cacheKey, item);

        return item;
    }
}
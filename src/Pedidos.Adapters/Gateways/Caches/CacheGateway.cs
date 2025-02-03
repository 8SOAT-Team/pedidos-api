namespace Pedidos.Adapters.Gateways.Caches;

public abstract class CacheGateway<TEntity>(ICacheContext cache)
{
    protected abstract Dictionary<string, Func<TEntity, (string cacheKey, bool InvalidateCacheOnChanges)>> CacheKeys
    {
        get;
    }

    protected Task InvalidateCacheOnChange(TEntity target)
    {
        Task[] invalidateTasks = [];

        foreach (var getCacheKey in CacheKeys.Keys.Select(key => CacheKeys[key]))
        {
            var (cacheKey, invalidateCacheOnChanges) = getCacheKey(target);
            if (!invalidateCacheOnChanges) continue;

            invalidateTasks = [.. invalidateTasks, cache.InvalidateCacheAsync(cacheKey)];
        }

        return invalidateTasks.Length > 0 ? Task.WhenAll(invalidateTasks) : Task.CompletedTask;
    }
}
using System.Diagnostics.CodeAnalysis;
using Pedidos.Adapters.Gateways.Caches;
using Pedidos.Apps.Clientes.Gateways;
using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Infrastructure.Clientes.Gateways;
[ExcludeFromCodeCoverage]
public record ClienteKey(string? Cpf = null);

public class ClienteGatewayCache(IClienteGateway nextExecution, ICacheContext cache)
    : CacheGateway<ClienteKey>(cache), IClienteGateway
{
    private readonly ICacheContext _cache = cache;

    protected override Dictionary<string, Func<ClienteKey, (string cacheKey, bool InvalidateCacheOnChanges)>>
        CacheKeys => new()
    {
        [nameof(GetClienteByCpfAsync)] =
            c => ($"{nameof(ClienteGatewayCache)}:{nameof(GetClienteByCpfAsync)}:{c.Cpf}", false)
    };

    public async Task<Cliente?> GetClienteByCpfAsync(Cpf cpf)
    {
        var getKey = CacheKeys[nameof(GetClienteByCpfAsync)];
        var cacheKey = getKey(new ClienteKey(cpf.GetSanitized())).cacheKey;

        var result = await _cache.GetItemByKeyAsync<Cliente>(cacheKey);

        if (result.HasValue) return result.Value;

        var client = await nextExecution.GetClienteByCpfAsync(cpf);
        _ = await _cache.SetNotNullStringByKeyAsync(cacheKey, client);

        return client;
    }

    public async Task<Cliente> InsertCliente(Cliente cliente)
    {
        var clienteInserido = await nextExecution.InsertCliente(cliente);

        var itemKey = $"{CacheKeys[nameof(GetClienteByCpfAsync)]}:{clienteInserido.Cpf.GetSanitized()}";
        await _cache.SetNotNullStringByKeyAsync(itemKey, clienteInserido);

        return clienteInserido;
    }
}
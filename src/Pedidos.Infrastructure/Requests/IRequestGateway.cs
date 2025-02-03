using System.Text.Json;
using Pedidos.Adapters.Gateways.Caches;

namespace Pedidos.Infrastructure.Requests;

public interface IRequestGateway
{
    Task<string?> GetRequest(string requestName);
    Task<T> CacheResponse<T>(string requestName, T response);
}

public class RequestGateway(ICacheContext cacheContext) : IRequestGateway
{
    private const string RequestKey = "Request:{0}";

    public async Task<string?> GetRequest(string requestName)
    {
        var result = await cacheContext.GetItemByKeyAsync<string>(string.Format(RequestKey, requestName));
        return result.IsSucceed ? result.Value : null;
    }

    public async Task<T> CacheResponse<T>(string requestName, T response)
    {
        await cacheContext.SetStringByKeyAsync(string.Format(RequestKey, requestName),
            JsonSerializer.Serialize(response), 300);

        return response;
    }
}
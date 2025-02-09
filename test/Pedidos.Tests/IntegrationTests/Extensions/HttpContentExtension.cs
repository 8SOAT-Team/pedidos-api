using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pedidos.Tests.IntegrationTests.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpContentExtension
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        MaxDepth = 16,
        ReferenceHandler = ReferenceHandler.Preserve,
        Converters = { new JsonStringEnumConverter() }
    };
    
    public static async Task<T?> ReadAsJsonAsync<T>(this HttpContent content)
    {
        var json = await content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, JsonOptions);
    }
}
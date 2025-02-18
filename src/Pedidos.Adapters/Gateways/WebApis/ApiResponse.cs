using System.Net;

namespace Pedidos.Adapters.Gateways.WebApis;

public record ApiResponse<TContent> : ApiResponse
{
    public TContent? Content { get; init; }
};

public record ApiResponse
{
    public HttpStatusCode StatusCode { get; init; }
    public bool IsSuccessStatusCode => (int)StatusCode >= 200 && (int)StatusCode <= 299;
    public ApiError? Error { get; init; }
};

public record ApiError
{
    public string Message { get; init; } = null!;
}
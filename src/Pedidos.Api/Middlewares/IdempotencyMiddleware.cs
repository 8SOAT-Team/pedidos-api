using Pedidos.Infrastructure.Requests;

namespace Pedidos.Api.Middlewares;

public class IdempotencyMiddleware
{
    private readonly ILogger<IdempotencyMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly IRequestGateway _requestGateway;

    public IdempotencyMiddleware(RequestDelegate next, IRequestGateway requestGateway,
        ILogger<IdempotencyMiddleware> logger)
    {
        _next = next;
        _requestGateway = requestGateway;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("X-RequestId", out var requestId))
        {
            await _next(context);
            return;
        }

        var cachedResponse = await _requestGateway.GetRequest(requestId.ToString());
        if (cachedResponse != null)
        {
            _logger.LogInformation("Requisição idempotente detectada. Retornando resposta do cache para {RequestId}",
                requestId);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(cachedResponse);
            return;
        }

        var originalBodyStream = context.Response.Body;
        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        await _next(context);

        var statusCode = context.Response.StatusCode;

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
        memoryStream.Seek(0, SeekOrigin.Begin);

        if (statusCode is >= 200 and < 300) await _requestGateway.CacheResponse(requestId.ToString(), responseBody);

        await memoryStream.CopyToAsync(originalBodyStream);
    }
}
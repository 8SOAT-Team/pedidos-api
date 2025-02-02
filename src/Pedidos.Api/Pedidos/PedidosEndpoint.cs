using System.Net;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Adapters.Controllers.Pedidos;
using Pedidos.Adapters.Controllers.Pedidos.Dtos;
using Pedidos.Adapters.Types.Results;
using Pedidos.Api.Dtos;
using Pedidos.Api.Endpoints;
using Pedidos.Api.Endpoints.Extensions;

namespace Pedidos.Api.Pedidos;

public static class PedidosEndpoint
{
    public static void AddEndpointPedidos(this WebApplication app, RouteGroupBuilder group)
    {
        const string pedidoTag = "Pedido";

        group.MapPost("/pedido", async ([FromHeader(Name = Constants.IdempotencyHeaderKey)] Guid? idempotencyKey,
                [FromServices] IPedidoController pedidoController,
                [FromBody] NovoPedidoDto request,
                HttpContext httpContext) =>
            {
                var pedidoCriado = await pedidoController.CreatePedidoAsync(request);

                IResult result = null!;

                pedidoCriado.Match(
                    onSuccess: (p) => result = Results.Created($"/pedido/{p.Id}", p),
                    onFailure: (errors) => result = pedidoCriado.GetFailureResult());

                return result;
            }).WithTags(pedidoTag)
            .Produces<PedidoDto>((int)HttpStatusCode.Created)
            .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithSummary("Crie um pedido informando os itens.")
            .WithOpenApi();;

        group.MapGet("/pedido/{id:guid}",
                async ([FromHeader(Name = Constants.IdempotencyHeaderKey)] Guid? idempotencyKey,
                    [FromServices] IPedidoController pedidoController, [FromRoute] Guid id) =>
                {
                    var pedido = await pedidoController.GetPedidoByIdAsync(id);
                    return pedido.GetResult();
                }).WithTags(pedidoTag)
            .Produces<PedidoDto>((int)HttpStatusCode.Created)
            .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithSummary("Obtenha um pedido")
            .WithOpenApi();;

        group.MapGet("/pedido",
                async ([FromHeader(Name = Constants.IdempotencyHeaderKey)] Guid? idempotencyKey,
                    [FromServices] IPedidoController pedidoController) =>
                {
                    var result = await pedidoController.GetAllPedidosAsync();
                    return result.GetResult();
                }).WithTags(pedidoTag)
            .Produces<List<PedidoDto>>((int)HttpStatusCode.Created)
            .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithSummary("Liste pedidos")
            .WithOpenApi();;

        group.MapGet("/pedido/status", async ([FromHeader(Name = Constants.IdempotencyHeaderKey)] Guid? idempotencyKey,
                [FromServices] IPedidoController pedidoController) =>
            {
                var pedidos = await pedidoController.GetAllPedidosPending();
                return pedidos.GetResult();
            }).WithTags(pedidoTag)
            .Produces<List<PedidoDto>>((int)HttpStatusCode.Created)
            .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithSummary("Lista de pedidos Pendentes (Pronto > Em Preparação > Recebido)")
            .WithOpenApi();;

        group.MapPut("/pedido/{id:guid}/status", async (
                [FromHeader(Name = Constants.IdempotencyHeaderKey)]
                Guid? idempotencyKey,
                [FromServices] IPedidoController pedidoController,
                [FromRoute] Guid id,
                [FromBody] AtualizarStatusDoPedidoDto request) =>
            {
                var result = await pedidoController.AtualizarStatusDePreparacaoDoPedido(request.NovoStatus, id);
                return result.GetResult();
            }).WithTags(pedidoTag)
            .Produces<PedidoDto>((int)HttpStatusCode.Created)
            .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithSummary("Atualize o status de um pedido")
            .WithOpenApi();
    }
}
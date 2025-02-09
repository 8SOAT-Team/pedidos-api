using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Adapters.Controllers.Clientes;
using Pedidos.Adapters.Controllers.Clientes.Dtos;
using Pedidos.Adapters.Types.Results;
using Pedidos.Api.Endpoints;
using Pedidos.Api.Endpoints.Extensions;

namespace Pedidos.Api.Clientes;

public static class ClienteEndpoint
{
    public static void AddEndpointClientes(this WebApplication app, RouteGroupBuilder group)
    {
        const string endpointTag = "Clientes";

        group.MapGet("/cliente", async (
                [FromHeader(Name = Constants.IdempotencyHeaderKey)]
                Guid? idempotencyKey,
                [FromServices] IClienteController controller,
                [FromQuery] [Required] string cpf) =>
            {
                var useCaseResult = await controller.IdentificarClienteAsync(cpf);
                return useCaseResult.GetResult();
            }).WithTags(endpointTag).WithSummary("Identifique um cliente pelo seu CPF")
            .Produces<ClienteIdentificadoDto>()
            .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithOpenApi();

        group.MapPost("/cliente", async (
                [FromHeader(Name = Constants.IdempotencyHeaderKey)]
                Guid? idempotencyKey,
                [FromServices] IClienteController controller,
                [FromBody] NovoClienteDto request) =>
            {
                var useCaseResult = await controller.CriarNovoClienteAsync(request);

                return useCaseResult.GetResult();
            }).WithTags(endpointTag).WithSummary("Cadastre um novo cliente")
            .Produces<ClienteIdentificadoDto>()
            .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
            .WithOpenApi();
    }
}
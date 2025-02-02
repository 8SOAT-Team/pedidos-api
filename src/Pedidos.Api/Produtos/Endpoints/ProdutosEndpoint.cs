using System.Net;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Adapters.Controllers.Produtos;
using Pedidos.Adapters.Types.Results;
using Pedidos.Api.Endpoints;
using Pedidos.Api.Endpoints.Extensions;
using Pedidos.Apps.Produtos.Enums;
using Pedidos.Apps.Produtos.UseCases.DTOs;

namespace Pedidos.Api.Produtos.Endpoints;

public static class ProdutosEndpoint
{
    public static void AddEndPointProdutos(this WebApplication app, RouteGroupBuilder group)
    {
        const string produtoTag = "Produto";
        const string categoriaTag = "Produto:Categoria";

        group.MapGet("/produto", async ([FromHeader(Name = Constants.IdempotencyHeaderKey)] Guid? idempotencyKey,
                [FromServices] IProdutoController controller) =>
            {
                var produtos = await controller.GetAllProdutosAsync();
                return produtos.GetResult();
            }).WithTags(produtoTag)
            .WithSummary("Obtenha a lista de todos os produtos cadastrados")
            .Produces<ICollection<ProdutoDto>>()
            .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithOpenApi();

        group.MapPost("/produto",
                async ([FromHeader(Name = Constants.IdempotencyHeaderKey)] Guid? idempotencyKey,
                    [FromServices] IProdutoController controller, [FromBody] NovoProdutoDto request) =>
                {
                    var produtoCriado = await controller.CreateProdutoAsync(request);
                    return produtoCriado.GetResult();
                }).WithTags(produtoTag)
            .WithSummary("Inclua novos produtos")
            .Produces<ProdutoCriadoDto>()
            .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithOpenApi();

        group.MapGet("/produto/categoria/{categoria}",
                async ([FromHeader(Name = Constants.IdempotencyHeaderKey)] Guid? idempotencyKey,
                    [FromServices] IProdutoController controller, [FromRoute] ProdutoCategoria categoria) =>
                {
                    var produtos = await controller.ListarProdutoPorCategoriaAsync(categoria);
                    return produtos.GetResult();
                }).WithTags(categoriaTag)
            .WithSummary("Liste todos os produtos de uma determinada categoria.")
            .Produces<ICollection<ProdutoDto>>()
            .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithOpenApi();


        group.MapGet("/produto/{id:guid}",
                async ([FromHeader(Name = Constants.IdempotencyHeaderKey)] Guid? idempotencyKey, [FromRoute] Guid id,
                    [FromServices] IProdutoController controller) =>
                {
                    var produto = await controller.GetProdutoByIdAsync(id);
                    return produto.GetResult();
                }).WithTags(produtoTag)
            .WithSummary("Obtenha um produto pelo seu identificador.")
            .Produces<ProdutoDto?>()
            .Produces<AppBadRequestProblemDetails>((int)HttpStatusCode.BadRequest)
            .Produces((int)HttpStatusCode.NotFound)
            .WithOpenApi();
    }
}
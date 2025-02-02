using Microsoft.Extensions.Logging;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Apps.Produtos.UseCases.DTOs;
using Pedidos.Apps.UseCases;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Domain.Produtos.Enums;

namespace Pedidos.Apps.Produtos.UseCases;

public class CriarProdutoUseCase(ILogger<CriarProdutoUseCase> logger, IProdutoGateway produtoGateway)
    : UseCase<CriarProdutoUseCase, NovoProdutoDto, Produto>(logger)
{
    protected override async Task<Produto?> Execute(NovoProdutoDto command)
    {
        var produto = new Produto(command.Nome, command.Descricao, command.Preco, command.Imagem,
            (ProdutoCategoria)command.Categoria);
        var produtoCriado = await produtoGateway.CreateProdutoAsync(produto);

        return produtoCriado;
    }
}
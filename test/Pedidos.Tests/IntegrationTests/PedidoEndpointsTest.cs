using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Pedidos.Adapters.Controllers.Pedidos.Dtos;
using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Tests.IntegrationTests.Builder;
using Pedidos.Tests.IntegrationTests.Extensions;
using Pedidos.Tests.IntegrationTests.HostTest;

namespace Pedidos.Tests.IntegrationTests;

public class PedidoEndpointsTest : IClassFixture<FastOrderWebApplicationFactory>
{
    private readonly FastOrderWebApplicationFactory _factory;

    public PedidoEndpointsTest(FastOrderWebApplicationFactory factory)
    {
        _factory = factory;
    }

    private async Task<Pedido> CriarPedido()
    {
        try
        {
            var cliente = _factory.Context!.Clientes.FirstOrDefault();

            if (cliente is null)
            {
                cliente = new ClienteBuilder().Build();
                _factory.Context.Clientes.Add(cliente);
                _factory.Context.SaveChanges();
            }

            var pedidoExistente = _factory.Context!.Pedidos.FirstOrDefault();
            if (pedidoExistente is null)
            {
                pedidoExistente = new PedidoBuilder(cliente.Id).Build();
                _factory.Context.Add(pedidoExistente);
                await _factory.Context.SaveChangesAsync();
            }


            return pedidoExistente;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }

    private async Task<Cliente> CriarCliente()
    {
        try
        {
            var clienteExistente = _factory.Context!.Clientes.FirstOrDefault();
            if (clienteExistente is null)
            {
                clienteExistente = new ClienteBuilder().Build();
                _factory.Context.Clientes.Add(clienteExistente);
                await _factory.Context.SaveChangesAsync();
            }
            return clienteExistente;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);

        }
    }

    private async Task<Produto> CriarProduto()
    {
        try
        {
            var produtoExistente = _factory.Context!.Produtos.FirstOrDefault();
            if (produtoExistente is null)
            {
                produtoExistente = new ProdutoBuilder().Build();
                _factory.Context.Produtos.Add(produtoExistente);
                await _factory.Context.SaveChangesAsync();
            }
            return produtoExistente;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    [Fact]
    public async Task POST_Deve_criar_pedido()
    {
        //Arrange
        var cliente = await CriarCliente();
        var produto = await CriarProduto();

        var pedidoDto = NovoPedidoDtoBuilder.CreateValid(
            (faker, builder) =>
            {
                return faker.Make(1, () => builder.WithProdutoId(produto.Id).Generate()).ToList();
            }, 
            cliente.Id);
        var httpClient = _factory.CreateClient();

        //Act
        var response = await httpClient.PostAsJsonAsync("/v1/pedido", pedidoDto);

        //Assert
        response.Should().NotBeNull();
        var content = await response.Content.ReadAsStringAsync();
        response.IsSuccessStatusCode.Should().BeTrue(because: "indica o sucesso da requisição. Porém retornou: {0}",
            response.StatusCode);
    }

    [Fact]
    public async Task POST_Nao_Deve_Criar_Pedido_Com_Dados_Invalidos()
    {
        //Arrange
        var pedidoDto = NovoPedidoDtoBuilder.CreateInvalid();
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.PostAsJsonAsync("/v1/pedido", pedidoDto);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GET_Deve_Retornar_Pedido_Pelo_Id()
    {
        //Arrange
        const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
        var cliente = new ClienteBuilder().Build();
        _factory.Context!.Clientes.Add(cliente);
        await _factory.Context.SaveChangesAsync();

        var pedidoExistente = new PedidoBuilder(cliente.Id).Build();
        _factory.Context.Add(pedidoExistente);
        await _factory.Context.SaveChangesAsync();

        var httpClient = _factory.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"/v1/pedido/{pedidoExistente.Id}");

        //Assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }

    [Fact]
    public async Task GET_Deve_Retornar_Todos_Pedidos()
    {
        //Arrange
        var cliente = new ClienteBuilder().Build();
        _factory.Context!.Clientes.Add(cliente);

        var pedidoExistente = new PedidoBuilder(cliente.Id).Build();
        _factory.Context.Add(pedidoExistente);
        await _factory.Context.SaveChangesAsync();
        var httpClient = _factory.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"/v1/pedido");

        //Assert
        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue(because: "indica o sucesso da requisição. Porém retornou: {0}",
            response.StatusCode);

        var produtoCriado = await response.Content.ReadAsJsonAsync<List<PedidoDto>>();
        produtoCriado.Should().NotBeNull().And.NotBeEmpty();
        produtoCriado.FirstOrDefault(p => p.Id == pedidoExistente.Id).Should().NotBeNull();
    }
}
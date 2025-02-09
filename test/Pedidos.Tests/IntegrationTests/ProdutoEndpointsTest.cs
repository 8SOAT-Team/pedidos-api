using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Pedidos.Apps.Produtos.UseCases.DTOs;
using Pedidos.Tests.IntegrationTests.Builder;
using Pedidos.Tests.IntegrationTests.Extensions;
using Pedidos.Tests.IntegrationTests.HostTest;

namespace Pedidos.Tests.IntegrationTests;

public class ProdutoEndpointsTest : IClassFixture<FastOrderWebApplicationFactory>
{
    private readonly FastOrderWebApplicationFactory _factory;

    public ProdutoEndpointsTest(FastOrderWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GET_Deve_buscar_produto_por_id()
    {
        //Arrange
        var produtoExistente = new ProdutoBuilder().Build();
        _factory.Context!.Add(produtoExistente);
        await _factory.Context.SaveChangesAsync();

        var httpClient = _factory.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"/v1/produto/{produtoExistente.Id}");

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue(because: "indica o sucesso da requisição. Porém retornou: {0}",
            response.StatusCode);

        var produtoCriado = await response.Content.ReadAsJsonAsync<ProdutoDto>();
        produtoCriado.Should().NotBeNull();
        produtoCriado.Id.Should().Be(produtoExistente.Id);
    }

    [Fact]
    public async Task POST_Deve_criar_produto()
    {
        //Arrange
        var produtoDto = new NovoProdutoDtoBuilder().Build();
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.PostAsJsonAsync("/v1/produto", produtoDto);
        //Assert
        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue(because: "indica o sucesso da requisição. Porém retornou: {0}",
            response.StatusCode);
    }

    [Fact]
    public async Task POST_Nao_Deve_Criar_Produto_Com_Dados_Invalidos()
    {
        //Arrange
        var produtoDto = new NovoProdutoDtoInvalidoBuilder().Build();
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.PostAsJsonAsync("/v1/produto", produtoDto);
        //Assert
        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeFalse(because: "indica a rejeição da requisição. Porém retornou: {0}",
            response.StatusCode);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GET_Deve_Retornar_Produto_Id_Categoria()
    {
        //Arrange
        var produtoExistente = new ProdutoBuilder().Build();
        _factory.Context!.Add(produtoExistente);
        await _factory.Context.SaveChangesAsync();
        
        var httpClient = _factory.CreateClient();
        
        //Act
        var response =
            await httpClient.GetAsync(
                $"/v1/produto/categoria/{produtoExistente.Categoria}");
        
        //Assert
        response.IsSuccessStatusCode.Should().BeTrue(because: "indica o sucesso da requisição. Porém retornou: {0}",
            response.StatusCode);
        var produtoCriado = await response.Content.ReadAsJsonAsync<ICollection<ProdutoDto>>();
        produtoCriado.Should().NotBeNull();
        produtoCriado.Should().HaveCountGreaterThanOrEqualTo(1);
    }
}
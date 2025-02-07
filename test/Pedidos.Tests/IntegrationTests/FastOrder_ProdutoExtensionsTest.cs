using System.Net;
using System.Net.Http.Json;
using Pedidos.Api.Dtos;
using Pedidos.Tests.IntegrationTests.Builder;
using Pedidos.Tests.IntegrationTests.HostTest;

namespace Pedidos.Tests.IntegrationTests;
public class FastOrder_ProdutoExtensionsTest : IClassFixture<FastOrderWebApplicationFactory>
{
    private readonly FastOrderWebApplicationFactory _factory;
    public FastOrder_ProdutoExtensionsTest(FastOrderWebApplicationFactory factory)
    {
        _factory = factory;
    }
    [Fact]
    public async Task GET_Deve_buscar_produto_por_id()
    {
        //Arrange
        var produtoExistente = _factory.Context!.Produtos.FirstOrDefault();
        if (produtoExistente is null)
        {
            produtoExistente = new ProdutoBuilder().Build();
            _factory.Context.Add(produtoExistente);
            _factory.Context.SaveChanges();
        }
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.GetFromJsonAsync<ProdutoDto>($"/produto/" + produtoExistente.Id);
        //Assert
        Assert.NotNull(response);
    }
    [Fact]
    public async Task POST_Deve_criar_produto()
    {
        //Arrange
        var produtoDto = new NovoProdutoDtoBuilder().Build();
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.PostAsJsonAsync("/produto", produtoDto);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    [Fact]
    public async Task POST_Nao_Deve_Criar_Produto_Com_Dados_Invalidos()
    {
        //Arrange
        var produtoDto = new NovoProdutoDtoInvalidoBuilder().Build();
        var httpClient = _factory.CreateClient();
        //Act
        var response = await httpClient.PostAsJsonAsync("/produto", produtoDto);
        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // [Fact]
    // public async Task GET_Deve_Retornar_Produto_Id_Categoria()
    // {
    //     //Arrange
    //     var produtoExistente = _factory.Context!.Produtos.FirstOrDefault();
    //     if (produtoExistente is null)
    //     {
    //         produtoExistente = new ProdutoBuilder().Build();
    //         _factory.Context.Add(produtoExistente);
    //         _factory.Context.SaveChanges();
    //     }
    //     var httpClient = _factory.CreateClient();
    //     //Act
    //     var response = await httpClient.GetFromJsonAsync<ICollection<ProdutoDTO>>($"/produto/categoria/{produtoExistente.CategoriaId}");
    //     //Assert
    //     Assert.NotNull(response);
    // }
    
    // [Fact]
    //
    // public async Task GET_Nao_Deve_Retornar_Produto_Categoria_Id_Invalido()
    // {
    //     //Arrange
    //     var httpClient = _factory.CreateClient();
    //     //Act
    //     var response = await httpClient.GetFromJsonAsync<ICollection<ProdutoDTO>>($"/produto/categoria/{Guid.NewGuid()}");
    //     //Assert
    //     Assert.NotNull(response);
    //     Assert.Equal(0, response.Count);
    //
    // }
    
}

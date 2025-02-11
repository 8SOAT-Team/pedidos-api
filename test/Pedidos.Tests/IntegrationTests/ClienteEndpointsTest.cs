using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Pedidos.Adapters.Controllers.Clientes.Dtos;
using Pedidos.Tests.IntegrationTests.Builder;
using Pedidos.Tests.IntegrationTests.HostTest;

namespace Pedidos.Tests.IntegrationTests;

public class ClienteEndpointsTest : IClassFixture<FastOrderWebApplicationFactory>
{
    private readonly FastOrderWebApplicationFactory _factory;

    public ClienteEndpointsTest(FastOrderWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GET_Deve_buscar_cliente_CPF()
    {
        //Arrange
        var clienteExistente = new ClienteBuilder().Build();
        _factory.Context!.Add(clienteExistente);
        await _factory.Context.SaveChangesAsync();

        var httpClient = _factory.CreateClient();

        //Act
        var response =
            await httpClient.GetAsync($"/v1/cliente?cpf={clienteExistente.Cpf}");

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var result = await response.Content.ReadFromJsonAsync<ClienteIdentificadoDto>();
        result.Should().NotBeNull();
        result.Id.Should().Be(clienteExistente.Id);
    }

    [Fact]
    public async Task GET_Nao_Deve_buscar_Cliente_CPF_Invalido()
    {
        //Arrange
        var httpClient = _factory.CreateClient();

        //Act
        var response = await httpClient.GetAsync($"/cliente?cpf=12345678901");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task POST_Deve_criar_cliente()
    {
        //Arrange
        var clienteDto = new NovoClienteDtoBuilder().Build();

        var httpClient = _factory.CreateClient();

        //Act
        var response = await httpClient.PostAsJsonAsync("/v1/cliente", clienteDto);

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue(because: "indica o sucesso da requisição, mas retornou: {0}",
            response.StatusCode);
        var clienteCriado = await response.Content.ReadFromJsonAsync<ClienteIdentificadoDto>();
        clienteCriado.Should().NotBeNull(because: "é esperado que o cliente criado não seja nulo");
        clienteCriado.Id.Should().NotBeEmpty(because: "o id é criado pelo servidor").And
            .NotBe(Guid.Empty, because: "o id deve ser válido");
        clienteCriado.Nome.Should().Be(clienteDto.Nome, because: "o nome do cliente deve ser igual ao informado");
    }

    [Fact]
    public async Task POST_Dado_CPF_Nulo_Nao_deve_criar_cliente_E_retornar_bad_request()
    {
        //Arrange
        var clienteDto = new NovoClienteDtoBuilderInvalido().Build();
        var httpClient = _factory.CreateClient();
        
        //Act
        var response = await httpClient.PostAsJsonAsync("/v1/cliente", clienteDto);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
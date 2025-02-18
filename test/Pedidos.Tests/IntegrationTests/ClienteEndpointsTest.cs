using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Pedidos.Adapters.Controllers.Clientes.Dtos;
using Pedidos.Domain.Clientes.Entities;
using Pedidos.Tests.IntegrationTests.Builder;
using Pedidos.Tests.IntegrationTests.HostTest;

namespace Pedidos.Tests.IntegrationTests;

public class ClienteEndpointsTest : IClassFixture<FastOrderWebApplicationFactory>
{
    private readonly FastOrderWebApplicationFactory _factory;
    private readonly Cliente _cliente;


    public ClienteEndpointsTest(FastOrderWebApplicationFactory factory)
    {
        _factory = factory;
        _cliente = CriarCliente().Result;
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

    [Fact]
    public async Task GET_Deve_buscar_cliente_CPF()
    {
        //Arrange           
        var httpClient = _factory.CreateClient();

        //Act
        var response =
            await httpClient.GetAsync($"/v1/cliente?cpf={_cliente.Cpf}");

        //Assert
        response.Should().NotBeNull();
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
        response.Should().NotBeNull();
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
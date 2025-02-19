using Pedidos.Adapters.Controllers.Clientes.Dtos;
using Pedidos.Apps.Types.Results;

namespace Pedidos.Adapters.Controllers.Clientes;

public interface IClienteController
{
    Task<Result<ClienteIdentificadoDto>> IdentificarClienteAsync(string document);
    Task<Result<ClienteIdentificadoDto>> CriarNovoClienteAsync(NovoClienteDto newCliente);
}
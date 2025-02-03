using Microsoft.Extensions.Logging;
using Pedidos.Adapters.Controllers.Clientes.Dtos;
using Pedidos.Adapters.Presenters.Clientes;
using Pedidos.Adapters.Presenters.UseCases;
using Pedidos.Adapters.Types.Results;
using Pedidos.Apps.Clientes.Dtos;
using Pedidos.Apps.Clientes.Gateways;
using Pedidos.Apps.Clientes.UseCases;
using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Adapters.Controllers.Clientes;

internal class ClienteController : IClienteController
{
    private readonly IClienteGateway _clienteGateway;
    private readonly ILoggerFactory _loggerFactory;

    public ClienteController(ILoggerFactory loggerFactory, IClienteGateway clienteGateway)
    {
        _loggerFactory = loggerFactory;
        _clienteGateway = clienteGateway;
    }

    public async Task<Result<ClienteIdentificadoDto>> IdentificarClienteAsync(string document)
    {
        var isCpfValid = Cpf.TryCreate(document, out var cpf);

        if (isCpfValid is false)
            return Result<ClienteIdentificadoDto>.Failure(new AppBadRequestProblemDetails("Cpf inválido", document));

        var useCase =
            new IdentificarClienteUseCase(_loggerFactory.CreateLogger<IdentificarClienteUseCase>(), _clienteGateway);

        var useCaseResult = await useCase.ResolveAsync(cpf);

        if (useCase.IsFailure)
            return Result<ClienteIdentificadoDto>.Failure(useCase.GetErrors().AdaptUseCaseErrors().ToList());

        return useCaseResult.HasValue
            ? Result<ClienteIdentificadoDto>.Succeed(ClientePresenter.AdaptClienteIdentificado(useCaseResult.Value!))
            : Result<ClienteIdentificadoDto>.Empty();
    }

    public async Task<Result<ClienteIdentificadoDto>> CriarNovoClienteAsync(NovoClienteDto newCliente)
    {
        var isCpfValid = Cpf.TryCreate(newCliente.Cpf, out var cpf);
        var isEmailvalid = EmailAddress.TryCreate(newCliente.Email, out var email);

        var isValid = isCpfValid && isEmailvalid;
        if (!isValid)
        {
            var errors = new List<AppProblemDetails>();

            if (!isCpfValid) errors.Add(new AppBadRequestProblemDetails("Cpf inválido", newCliente.Cpf));

            if (!isEmailvalid) errors.Add(new AppBadRequestProblemDetails("Email inválido", newCliente.Email));

            return Result<ClienteIdentificadoDto>.Failure(errors);
        }

        var useCase =
            new CriarNovoClienteUseCase(_loggerFactory.CreateLogger<CriarNovoClienteUseCase>(), _clienteGateway);
        var useCaseResult = await useCase.ResolveAsync(new CriarNovoClienteDto(cpf, newCliente.Nome, email));

        return ControllerResultBuilder<ClienteIdentificadoDto, Cliente>
            .ForUseCase(useCase)
            .WithResult(useCaseResult)
            .AdaptUsing(ClientePresenter.AdaptClienteIdentificado)
            .Build();
    }
}
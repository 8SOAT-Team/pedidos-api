using CleanArch.UseCase;
using CleanArch.UseCase.Faults;
using CleanArch.UseCase.Options;
using Pedidos.Adapters.Presenters.UseCases;
using Pedidos.Adapters.Types.Results;

namespace Pedidos.Adapters.Controllers;

public class ControllerResultBuilder<TResultValue, TEntity>
{
    private const UseCaseErrorType BadRequest = UseCaseErrorType.BadRequest;

    private readonly bool _isFailure;
    private readonly UseCaseError? _useCaseError;
    private Func<TEntity, TResultValue>? _adapt;
    private string? _instance;
    private Any<TEntity>? _useCaseResult;

    private ControllerResultBuilder(IUseCase useCase)
    {
        _isFailure = useCase.IsFailure;
        _useCaseError = useCase.GetErrors().FirstOrDefault(e => e.Code == BadRequest) ??
                        useCase.GetErrors().FirstOrDefault();
    }

    public static ControllerResultBuilder<TResultValue, TEntity> ForUseCase(IUseCase useCase)
    {
        return new ControllerResultBuilder<TResultValue, TEntity>(useCase);
    }

    public ControllerResultBuilder<TResultValue, TEntity> WithResult(Any<TEntity> useCaseResult)
    {
        if (_isFailure) return this;

        _useCaseResult = useCaseResult;

        return this;
    }

    public ControllerResultBuilder<TResultValue, TEntity> WithInstance(Guid instance)
    {
        return WithInstance(instance.ToString());
    }

    public ControllerResultBuilder<TResultValue, TEntity> WithInstance(string instance = null!)
    {
        if (!_isFailure) _instance = instance;

        return this;
    }

    public ControllerResultBuilder<TResultValue, TEntity> AdaptUsing(Func<TEntity, TResultValue> adapt)
    {
        _adapt = adapt;
        return this;
    }

    public Result<TResultValue> Build()
    {
        if (_isFailure)
            return _useCaseError!.Code == BadRequest
                ? Result<TResultValue>.Failure(new AppBadRequestProblemDetails(_useCaseError.Description, _instance!))
                : Result<TResultValue>.Failure(_useCaseError.AdaptUseCaseError());

        return _useCaseResult!.HasValue
            ? Result<TResultValue>.Succeed(_adapt!(_useCaseResult.Value!))
            : Result<TResultValue>.Empty();
    }
}
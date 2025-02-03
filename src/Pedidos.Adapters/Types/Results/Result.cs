namespace Pedidos.Adapters.Types.Results;

public class Result<TValue>
{
    private Result(TValue value)
    {
        Value = value;
        HasValue = true;
    }

    private Result(List<AppProblemDetails> details)
    {
        ProblemDetails = details;
    }

    private Result()
    {
    }

    public TValue? Value { get; }

    public List<AppProblemDetails> ProblemDetails { get; } = [];

    public bool IsFailure => ProblemDetails.Count > 0;

    public bool IsSucceed => !IsFailure;

    public bool HasValue { get; }

    public static Result<TValue> Succeed(TValue value)
    {
        return new Result<TValue>(value);
    }

    public static Result<TValue> Failure(AppProblemDetails details)
    {
        return new Result<TValue>([details]);
    }

    public static Result<TValue> Failure(List<AppProblemDetails> details)
    {
        return new Result<TValue>(details);
    }

    public static Result<TValue> Empty()
    {
        return new Result<TValue>();
    }
}

public static class ResultExtension
{
    public static void Match<T>(this Result<T> result, Action<T> onSuccess,
        Action<List<AppProblemDetails>> onFailure)
    {
        if (result.HasValue)
        {
            onSuccess(result.Value!);
            return;
        }

        onFailure(result.ProblemDetails);
    }
}
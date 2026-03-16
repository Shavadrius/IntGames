namespace IntGames.Domain.Abstractions;

public static class ResultExtensions
{
    public static Result<TOut> Bind<TIn, TOut>(
        this Result<TIn> result, Func<TIn, Result<TOut>> next)
    {
        return result.IsSuccess ? next(result.Value) : Result.Failure<TOut>(result.Error);
    }

    public static Result<TOut> Map<TIn, TOut>(
        this Result<TIn> result, Func<TIn, TOut> mapper)
    {
        return result.IsSuccess ? Result.Success(mapper(result.Value)) : Result.Failure<TOut>(result.Error);
    }

    public static Result<TOut?> CreateOptional<TIn, TOut>(
        TIn? value, Func<TIn, Result<TOut>> factory)
        where TIn : class
        where TOut : class
    {
        if (value is null) return Result.Success<TOut?>(null);
        var result = factory(value);
        return result.IsSuccess
            ? Result.Success<TOut?>(result.Value)
            : Result.Failure<TOut?>(result.Error);
    }

    public static Result<TOut?> CreateOptional<TIn, TOut>(
        TIn? value, Func<TIn, Result<TOut>> factory)
        where TIn : struct
        where TOut : class
    {
        if (value is null) return Result.Success<TOut?>(null);
        var result = factory(value.Value);
        return result.IsSuccess
            ? Result.Success<TOut?>(result.Value)
            : Result.Failure<TOut?>(result.Error);
    }
}

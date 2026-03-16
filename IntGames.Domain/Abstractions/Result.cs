namespace IntGames.Domain.Abstractions;

public class Result
{
    public Result(bool isSuccess, IntGamesError error)
    {
        if (isSuccess && error != IntGamesError.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == IntGamesError.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public IntGamesError Error { get; }
    public bool IsFailure => !IsSuccess;

    public static Result Success() => new(true, IntGamesError.None);
    public static Result<T> Success<T>(T value) => new(value, true, IntGamesError.None);
    public static Result Failure(IntGamesError error) => new(false, error);
    public static Result<T> Failure<T>(IntGamesError error) => new(default, false, error);

    public static Result<T> Create<T>(T? value) =>
        value is not null ? Success(value) : Failure<T>(IntGamesError.NullValue);

    //extensions
    public static Result<(T1, T2)> Combine<T1, T2>(
    Result<T1> r1, Result<T2> r2)
    {
        if (r1.IsFailure) return Failure<(T1, T2)>(r1.Error);
        if (r2.IsFailure) return Failure<(T1, T2)>(r2.Error);
        return Success((r1.Value, r2.Value));
    }

    public static Result<(T1, T2, T3)> Combine<T1, T2, T3>(
        Result<T1> r1, Result<T2> r2, Result<T3> r3)
    {
        if (r1.IsFailure) return Failure<(T1, T2, T3)>(r1.Error);
        if (r2.IsFailure) return Failure<(T1, T2, T3)>(r2.Error);
        if (r3.IsFailure) return Failure<(T1, T2, T3)>(r3.Error);
        return Success((r1.Value, r2.Value, r3.Value));
    }

    public static Result<(T1, T2, T3, T4)> Combine<T1, T2, T3, T4>(
        Result<T1> r1, Result<T2> r2, Result<T3> r3, Result<T4> r4)
    {
        if (r1.IsFailure) return Failure<(T1, T2, T3, T4)>(r1.Error);
        if (r2.IsFailure) return Failure<(T1, T2, T3, T4)>(r2.Error);
        if (r3.IsFailure) return Failure<(T1, T2, T3, T4)>(r3.Error);
        if (r4.IsFailure) return Failure<(T1, T2, T3, T4)>(r4.Error);
        return Success((r1.Value, r2.Value, r3.Value, r4.Value));
    }
}

public class Result<T>(T? value, bool isSuccess, IntGamesError error) : Result(isSuccess, error)
{
    private readonly T? _value = value;

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("failed result has no data.");

    public static implicit operator Result<T>(T? value) => Create(value);
    public static implicit operator Result<T>(IntGamesError error) => Failure<T>(error);
}

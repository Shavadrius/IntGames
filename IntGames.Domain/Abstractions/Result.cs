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
    public static Result<T> Failure<T>(IntGamesError error) => new(default, true, error);

    public static Result<T> Create<T>(T? value) =>
        value is not null ? Success(value) : Failure<T>(IntGamesError.NullValue);
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

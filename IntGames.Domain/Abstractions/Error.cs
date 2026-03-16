namespace IntGames.Domain.Abstractions;

public record IntGamesError(string Code, string Message, ErrorType ErrorType)
{
    public static readonly IntGamesError None = new(string.Empty, string.Empty, ErrorType.None);
    public static readonly IntGamesError NullValue = new("Error.NullValue", "Null value was provided", ErrorType.Validation);
    
    public static IntGamesError Validation(string field, string message) => new($"Validation.{field}", message, ErrorType.Validation);
}

public enum ErrorType
{
    None = 0,
    Validation = 1,
    NotFound = 2,
}

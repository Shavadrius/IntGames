namespace IntGames.Domain.Abstractions;

public record IntGamesError(string Code, string Message)
{
    public static readonly IntGamesError None = new(string.Empty, string.Empty);
    public static readonly IntGamesError NullValue = new("Error.NullValue", "Null value was provided");
}

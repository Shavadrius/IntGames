using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Shared;

public record FirstName
{
    public static IntGamesError Invalid => IntGamesError.Validation("FirstName", "First Name is null or empty.");
    protected FirstName(string value) => Value = value;

    public static Result<FirstName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Invalid;
        }
        return new FirstName(value);
    }

    public string Value { get; private init; }
}
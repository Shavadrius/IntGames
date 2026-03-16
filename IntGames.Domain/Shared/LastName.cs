using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Shared;

public record LastName
{
    public static IntGamesError Invalid => IntGamesError.Validation("LastName", "Last Name is null or empty.");
    protected LastName(string value) => Value = value;

    public static Result<LastName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Invalid;
        }
        return new LastName(value);
    }

    public string Value { get; private init; }
}
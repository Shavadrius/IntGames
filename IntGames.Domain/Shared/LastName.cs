using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Shared;

public record LastName
{
    public static IntGamesError Invalid => IntGamesError.Validation("LastName", "Last Name is null or empty.");
    private LastName(string value) => Value = value;

    public static Result<LastName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Invalid;
        }
        return new LastName(value);
    }

    public string Value { get; private init; }
}

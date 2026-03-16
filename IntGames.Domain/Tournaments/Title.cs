using IntGames.Domain.Abstractions;
using IntGames.Domain.Shared;

namespace IntGames.Domain.Tournaments;

public record Title
{
    public static IntGamesError Invalid => IntGamesError.Validation("Title", "Title of tournament is null or empty.");
    private Title(string value) => Value = value;

    public static Result<Title> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Invalid;
        }
        return new Title(value);
    }

    public string Value { get; private init; }
}

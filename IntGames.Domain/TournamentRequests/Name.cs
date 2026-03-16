using IntGames.Domain.Abstractions;
using IntGames.Domain.Shared;

namespace IntGames.Domain.TournamentRequests;

public record Name
{
    public static IntGamesError Invalid => IntGamesError.Validation("Name", "Name is null or empty.");
    private Name(string value) => Value = value;

    public static Result<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Invalid;
        }
        return new Name(value);
    }

    public string Value { get; private init; }
}

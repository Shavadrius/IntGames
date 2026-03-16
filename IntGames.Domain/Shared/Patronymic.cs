using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Shared;

public record Patronymic
{
    public static IntGamesError Invalid => IntGamesError.Validation("Patronymic", "Patronymic is null or empty.");
    protected Patronymic(string value) => Value = value;

    public static Result<Patronymic> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Invalid;
        }
        return new Patronymic(value);
    }

    public string Value { get; private init; }
}
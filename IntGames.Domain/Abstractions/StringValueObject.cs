namespace IntGames.Domain.Abstractions;

public record StringValueObject
{
    public static IntGamesError Invalid => IntGamesError.Validation("Value", "String is null or empty.");
    protected StringValueObject(string value) => Value = value;

    public static Result<StringValueObject> Create (string value) {
        if (string.IsNullOrEmpty(value))
        {
            return Invalid;
        }
        return new StringValueObject(value);
    }

    public string Value { get; private init; }
}

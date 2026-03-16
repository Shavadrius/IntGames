using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Shared;

public record FirstName(string Value) : StringValueObject(Value);
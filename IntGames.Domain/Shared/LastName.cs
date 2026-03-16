using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Shared;

public record LastName(string Value) : StringValueObject(Value);
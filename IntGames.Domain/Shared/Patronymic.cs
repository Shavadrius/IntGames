using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Shared;

public record Patronymic(string Value) : StringValueObject(Value);
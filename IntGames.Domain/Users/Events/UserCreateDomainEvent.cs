using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Users.Events;

public sealed record UserCreateDomainEvent(Guid UserId) : IDomainEvent;

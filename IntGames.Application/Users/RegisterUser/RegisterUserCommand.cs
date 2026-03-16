using IntGames.Application.Abstractions.CQRS;

namespace IntGames.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email, 
    string Password,
    string? FirstName = null, 
    string? LastName = null, 
    string? Patronymic = null) : ICommand<Guid>;

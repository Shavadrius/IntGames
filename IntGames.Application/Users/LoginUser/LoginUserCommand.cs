using IntGames.Application.Abstractions.CQRS;

namespace IntGames.Application.Users.LoginUser;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;

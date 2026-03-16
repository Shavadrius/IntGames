using IntGames.Domain.Abstractions;
using IntGames.Domain.Users;

namespace IntGames.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<Result<string>> RegisterAsync(User user, string password, CancellationToken cancellationToken = default);
}

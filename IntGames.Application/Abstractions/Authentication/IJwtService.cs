using IntGames.Application.Users.LoginUser;
using IntGames.Domain.Abstractions;

namespace IntGames.Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<AccessTokenResponse>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
}

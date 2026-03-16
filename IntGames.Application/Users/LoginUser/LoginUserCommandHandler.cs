using IntGames.Application.Abstractions.Authentication;
using IntGames.Application.Abstractions.CQRS;
using IntGames.Domain.Abstractions;

namespace IntGames.Application.Users.LoginUser;

internal sealed class LoginUserCommandHandler(
    IJwtService jwtService) : ICommandHandler<LoginUserCommand, AccessTokenResponse>
{
    private readonly IJwtService _jwtService = jwtService;

    public Task<Result<AccessTokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return _jwtService.GetAccessTokenAsync(
            request.Email,
            request.Password,
            cancellationToken);
    }
}

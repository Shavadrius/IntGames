using IntGames.Application.Abstractions.Authentication;
using IntGames.Application.Abstractions.CQRS;
using IntGames.Domain.Abstractions;
using IntGames.Domain.Shared;
using IntGames.Domain.Users;

namespace IntGames.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IAuthenticationService authenticationService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request, 
        CancellationToken cancellationToken)
    {
        var userCreationRequest = User.Create(
            Email.Create(request.Email).Value,
            request.FirstName is null ? null : FirstName.Create(request.FirstName).Value,
            request.LastName is null ? null : LastName.Create(request.LastName).Value,
            request.Patronymic is null ? null : Patronymic.Create(request.Patronymic).Value);

        var user = userCreationRequest.Value;

        var registerUserRequest = await _authenticationService.RegisterAsync(
            user,
            request.Password,
            cancellationToken);

        user.SetIdentityId(registerUserRequest.Value);

        _userRepository.Add(user);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}

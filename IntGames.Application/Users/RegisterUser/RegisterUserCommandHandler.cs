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
        var createEmailResult = Email.Create(request.Email);
        var createFirstNameResult = request.FirstName is not null ? FirstName.Create(request.FirstName) : null;
        var createLastNameResult = request.LastName is not null ? LastName.Create(request.LastName) : null;
        var createPatronymicResult = request.Patronymic is not null ? Patronymic.Create(request.Patronymic) : null;

        if (createEmailResult.IsFailure)
            return Result.Failure<Guid>(createEmailResult.Error);

        if (createFirstNameResult?.IsFailure == true)
            return Result.Failure<Guid>(createFirstNameResult.Error);

        if (createLastNameResult?.IsFailure == true)
            return Result.Failure<Guid>(createLastNameResult.Error);

        if (createPatronymicResult?.IsFailure == true)
            return Result.Failure<Guid>(createPatronymicResult.Error);

        var createUserResult = User.Create(
            createEmailResult.Value,
            createFirstNameResult?.Value,
            createLastNameResult?.Value,
            createPatronymicResult?.Value);

        if (createUserResult.IsFailure)
        {
            return Result.Failure<Guid>(createUserResult.Error);
        }

        var user = createUserResult.Value;

        var registerUserResult = await _authenticationService.RegisterAsync(
            user,
            request.Password,
            cancellationToken);

        if (registerUserResult.IsFailure)
        {
            return Result.Failure<Guid>(registerUserResult.Error);
        }

        user.SetIdentityId(registerUserResult.Value);

        _userRepository.Add(user);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}

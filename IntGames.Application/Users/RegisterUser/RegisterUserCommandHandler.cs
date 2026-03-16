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
        var createFieldsResult = Result.Combine(
                Email.Create(request.Email),
                ResultExtensions.CreateOptional(request.FirstName, FirstName.Create),
                ResultExtensions.CreateOptional(request.LastName, LastName.Create),
                ResultExtensions.CreateOptional(request.Patronymic, Patronymic.Create));

        if (createFieldsResult.IsFailure)
        {
            return Result.Failure<Guid>(createFieldsResult.Error);
        }

        (Email email, FirstName? firstName, LastName? lastName, Patronymic? patronymic) = createFieldsResult.Value;

        var userResult = User.Create(email, firstName, lastName, patronymic);

        if (userResult.IsFailure)
        {
            return Result.Failure<Guid>(userResult.Error);
        }

        var user = userResult.Value;

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

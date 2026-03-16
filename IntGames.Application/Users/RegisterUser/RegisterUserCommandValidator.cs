using FluentValidation;

namespace IntGames.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Email).EmailAddress();

        RuleFor(c => c.Password).NotEmpty().MinimumLength(8);
    }
}

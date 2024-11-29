using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Auth;

public class LoginValidator : Validator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}
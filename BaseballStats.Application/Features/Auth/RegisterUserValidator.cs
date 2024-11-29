using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Auth;

public class RegisterUserValidator : Validator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithName("Username is required");
        RuleFor(x => x.Password).NotEmpty().WithName("Password is required");
        RuleFor(x => x.Role).NotEmpty().WithName("Role is required");
    }
}
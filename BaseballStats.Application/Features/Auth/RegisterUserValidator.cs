using FastEndpoints;
using FluentValidation;
using BaseballStats.Application.Mappers;

namespace BaseballStats.Application.Features.Auth;

public class RegisterUserValidator : Validator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithName("Username is required");
        RuleFor(x => x.Password).NotEmpty().WithName("Password is required");
        RuleFor(x => x.UserType).NotEmpty().WithName("Role is required");
        RuleFor(x => x).Must(ValidateUserType).WithMessage("Role is invalid");
    }

    private static bool ValidateUserType(RegisterUserCommand command)
    {
        try 
        {
            command.UserType.ToUserType();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
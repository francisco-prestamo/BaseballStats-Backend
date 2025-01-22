using BaseballStats.Application.Features.User.Put;
using BaseballStats.Application.Mappers;
using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.User.Put;

public class UpdateUserValidator : Validator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required");

        RuleFor(x => x.UserType)
            .NotEmpty().WithMessage("UserType is required");

        RuleFor(x => x)
            .Must(ValidateUserType).WithMessage("UserType is invalid");
    }

    private bool ValidateUserType(UpdateUserCommand command)
    {
        var userType = command.UserType;

        try
        {
            userType.ToUserType();
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
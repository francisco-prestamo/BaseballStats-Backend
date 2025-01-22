using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.User.Delete;

public class DeleteUserValidator : Validator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}
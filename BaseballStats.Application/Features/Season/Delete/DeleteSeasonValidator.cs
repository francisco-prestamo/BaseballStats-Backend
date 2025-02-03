using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Season.Delete;

public class DeleteSeasonValidator : Validator<DeleteSeasonCommand>
{
    public DeleteSeasonValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
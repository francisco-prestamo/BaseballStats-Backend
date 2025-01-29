using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Season.Put;

public class PutSeasonValidator : Validator<PutSeasonCommand>
{
    public PutSeasonValidator()
    {
        RuleFor(x => x.SeasonId)
            .NotEmpty().WithMessage("Season Id is required.")
            .GreaterThan(0).WithMessage("Season Id must be greater than 0.");

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("New Id is required.")
            .GreaterThan(0).WithMessage("New Id must be greater than 0.");
    }
}
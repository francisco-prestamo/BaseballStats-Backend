using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Series.Delete;

public class DeleteSeriesValidator : Validator<DeleteSeriesCommand>
{
    public DeleteSeriesValidator()
    {
        RuleFor(x => x.SeasonId)
            .NotEmpty().WithMessage("SeasonId is required.")
            .GreaterThan(0).WithMessage("SeasonId must be greater than 0.");

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
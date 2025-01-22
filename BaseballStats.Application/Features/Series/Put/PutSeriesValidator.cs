using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Series.Put;

public class PutSeriesValidator : Validator<PutSeriesCommand>
{
    public PutSeriesValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("SeriesId is required")
            .GreaterThan(0).WithMessage("SeriesId must be greater than 0");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required");

        RuleFor(x => x.StartDate).NotEmpty().WithMessage("StartDate is required");

        RuleFor(x => x.EndDate).NotEmpty().WithMessage("EndDate is required");

        RuleFor(x => x.SeasonId)
            .NotEmpty().WithMessage("IdSeason is required")
            .GreaterThan(0).WithMessage("IdSeason must be greater than 0");
    }
}
using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Game.GetGamesFromSeries;

public class GetGamesFromSeriesValidator : Validator<GetGamesFromSeriesCommand>
{
    public GetGamesFromSeriesValidator()
    {
        RuleFor(x => x.SeasonId)
            .NotEmpty().WithMessage("SeasonId is required")
            .GreaterThan(0).WithMessage("SeasonId must be greater than 0");

        RuleFor(x => x.SeriesId)
            .NotEmpty().WithMessage("SeriesId is required")
            .GreaterThan(0).WithMessage("SeriesId must be greater than 0");
    }
}
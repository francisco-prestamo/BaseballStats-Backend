using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Season.GetSeriesFromSeason
{
    public class GetSeriesFromSeasonValidator : Validator<GetSeriesFromSeasonCommand>
    {
        public GetSeriesFromSeasonValidator()
        {
            RuleFor(x => x.SeasonId)
                .NotEmpty().WithMessage("SeasonId is required")
                .GreaterThan(0).WithMessage("SeasonId must be greater than 0");
        }
    }
}
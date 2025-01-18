using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Team.GetTeamGamesInThisSeries;

public class GetTeamGamesInThisSeriesValidator : Validator<GetTeamGamesInThisSeriesCommand>
{
    public GetTeamGamesInThisSeriesValidator()
    {
        RuleFor(x => x.TeamId)
            .NotEmpty().WithMessage("TeamId is required")
            .GreaterThan(0).WithMessage("TeamId must be greater than 0");

        RuleFor(x => x.SeasonId)
            .NotEmpty().WithMessage("SeasonId is required")
            .GreaterThan(0).WithMessage("SeasonId must be greater than 0");
        
        RuleFor(x => x.SeriesId)
            .NotEmpty().WithMessage("SeriesId is required")
            .GreaterThan(0).WithMessage("SeriesId must be greater than 0");
    }
}
using FluentValidation;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInPosition.GetTeamInSeriesPlayerInPositions;

public class GetTeamInSeriesPlayerInPositionsValidator : Validator<GetTeamInSeriesPlayerInPositionsCommand>
{
    public GetTeamInSeriesPlayerInPositionsValidator()
    {
        RuleFor(x => x.SeriesId).NotEmpty().When(x => x.SeriesId != 0)
            .WithMessage("SeriesId is required");

        RuleFor(x => x.SeasonId).NotEmpty().When(x => x.SeasonId != 0)
            .WithMessage("SeasonId is required");

        RuleFor(x => x.TeamId).NotEmpty().When(x => x.TeamId != 0)
            .WithMessage("TeamId is required");
    }
}
using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.PlayerInSeries.Put;

public class PutPlayerInSeriesValidator : Validator<PutPlayerInSeriesCommand>
{
    public PutPlayerInSeriesValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty().When(x => x.PlayerId != 0)
            .WithMessage("PlayerId is required");
        RuleFor(x => x.SerieId).NotEmpty().When(x => x.SerieId != 0)
            .WithMessage("SerieId is required");
        RuleFor(x => x.SeasonId).NotEmpty().When(x => x.SeasonId != 0)
            .WithMessage("SeasonId is required");
        RuleFor(x => x.TeamId).NotEmpty().When(x => x.TeamId != 0)
            .WithMessage("TeamId is required");
    }

}
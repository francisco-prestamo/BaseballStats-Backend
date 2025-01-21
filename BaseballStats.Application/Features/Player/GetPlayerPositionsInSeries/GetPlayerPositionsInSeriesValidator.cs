using BaseballStats.Application.Features.Player.GetPlayerPositionsInSeries;
using FastEndpoints;
using FluentValidation;

public class GetPlayerPositionsInSeriesValidator : Validator<GetPlayerPositionsInSeriesCommand>
{
    public GetPlayerPositionsInSeriesValidator()
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty().WithMessage("PlayerId is required");

        RuleFor(x => x.SeasonId)
            .NotEmpty().WithMessage("SeasonId is required");

        RuleFor(x => x.SeriesId)
            .NotEmpty().WithMessage("SeriesId is required");
    }
}
using FastEndpoints;
using FluentValidation;
using BaseballStats.Application.Utilities;

namespace BaseballStats.Application.Features.StarPlayerInPosition.Delete;

public class DeleteStarPlayerInPositionValidator : Validator<DeleteStarPlayerInPositionCommand>
{
    public DeleteStarPlayerInPositionValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty().When(x => x.PlayerId != 0)
            .WithMessage("PlayerId is required");
        // RuleFor(x => x.SeriesId).GreaterThan(0);
        // RuleFor(x => x.SeasonId).GreaterThan(0);
        // RuleFor(x => x.Position).NotEmpty();

        RuleFor(x => x.SeriesId).NotEmpty().When(x => x.SeriesId != 0)
            .WithMessage("SeriesId is required");

        RuleFor(x => x.SeasonId).NotEmpty().When(x => x.SeasonId != 0)
            .WithMessage("SeasonId is required");
        
        RuleFor(x => x.Position).NotEmpty()
            .WithMessage("Position is required");

        RuleFor(x => x.Position).Must(x => x.IsValidPosition())
            .WithMessage("Position is invalid");
    }
}
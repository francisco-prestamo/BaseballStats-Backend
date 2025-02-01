using FastEndpoints;
using FluentValidation;
using BaseballStats.Application.Utilities;

namespace BaseballStats.Application.Features.StarPlayerInPosition.Post;

class PostStarPlayerInPositionValidator : Validator<PostStarPlayerInPositionCommand>
{
    public PostStarPlayerInPositionValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty().When(x => x.PlayerId != 0)
            .WithMessage("PlayerId is required.");
        RuleFor(x => x.Position).Must(x => x.IsValidPosition())
            .WithMessage("Position is invalid.");
        RuleFor(x => x.SeriesId).NotEmpty().When(x => x.SeriesId != 0)
            .WithMessage("SeriesId is required.");
        RuleFor(x => x.SeasonId).NotEmpty().When(x => x.SeasonId != 0)
            .WithMessage("SeasonId is required.");
    }
}

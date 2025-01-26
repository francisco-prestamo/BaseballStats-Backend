using BaseballStats.Domain.Enums;
using BaseballStats.Application.Utilities;
using FastEndpoints;
using FluentValidation;
using BaseballStats.Application.Mappers;

namespace BaseballStats.Application.Features.PlayerInPosition.Post;

public class PostPlayerInPositionValidator : Validator<PostPlayerInPositionCommand>
{
    public PostPlayerInPositionValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty().When(x => x.PlayerId != 0)
            .WithMessage("PlayerId is required");
        RuleFor(x => x.Position).Must(x => x.IsValidPosition())
            .WithMessage("Position name is invalid");
        RuleFor(x => x).Must(ValidateNonPitcher)
            .WithMessage("Player cannot be assigned to pitcher position without extra data");
        RuleFor(x => x.Position).NotEmpty()
            .WithMessage("Position is required");
        RuleFor(x => x.Effectiveness).NotEmpty().When(x => x.Effectiveness != 0)
            .WithMessage("Effectiveness is required");
    }

    private bool ValidateNonPitcher(PostPlayerInPositionCommand command)
    {
        try
        {
            var position = command.Position.GetPlayerPosition();
            return position != PlayerPositions.Pitcher;
        }
        catch
        {
            return true;
        }
    }
}
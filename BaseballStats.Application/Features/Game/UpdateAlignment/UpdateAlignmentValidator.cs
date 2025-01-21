using BaseballStats.Application.Mappers;
using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Game.UpdateAlignment;

public class UpdateAlignmentValidator : Validator<UpdateAlignmentCommand>
{
    public UpdateAlignmentValidator()
    {
        RuleFor(x => x.GameId)
            .NotEmpty().WithMessage("GameId is required.")
            .GreaterThan(0).WithMessage("GameId must be greater than 0.");

        RuleFor(x => x.TeamId)
            .NotEmpty().WithMessage("TeamId is required.")
            .GreaterThan(0).WithMessage("TeamId must be greater than 0.");

        RuleFor(x => x.Alignment)
            .NotEmpty().WithMessage("PlayersInPosition is required.");
    
        RuleFor(x => x)
            .Must(ValidatePositionNames).WithMessage("Invalid position name.");
    
        RuleFor(x => x)
            .Must(ValidatePositionUnicity).WithMessage("Position must be assigned only once.");

        RuleFor(x => x)
            .Must(ValidatePlayerUnicity).WithMessage("Player must be assigned only once.");
    }

    private static bool ValidatePositionNames(UpdateAlignmentCommand command)
    {
        var positions =
            from pis in command.Alignment
            group pis by pis.Position into g
            select g.Key;

        foreach (var position in positions)
        {
            try{
                var playerPosition = position.GetPlayerPosition();
            }
            catch (ArgumentException){
                return false;
            }
        }

        return true;
    }

    private static bool ValidatePositionUnicity(UpdateAlignmentCommand command)
    {
        var positionCounts =
            from pis in command.Alignment
            group pis by pis.Position into g
            select g.Count();

        return positionCounts.All(x => x == 1);
    }

    private static bool ValidatePlayerUnicity(UpdateAlignmentCommand command)
    {
        var playerCounts =
            from pis in command.Alignment
            group pis by pis.Player.Id into g
            select g.Count();

        return playerCounts.All(x => x == 1);
    }
}

    

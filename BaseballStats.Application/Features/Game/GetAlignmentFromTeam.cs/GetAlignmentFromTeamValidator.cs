using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Game.GetAlignmentFromTeam;

public class GetAlignmentFromTeamValdator : Validator<GetAlignmentFromTeamCommand>
{
    public GetAlignmentFromTeamValdator()
    {
        RuleFor(x => x.TeamId)
            .NotEmpty().WithMessage("TeamId is required");

        RuleFor(x => x.GameId)
            .NotEmpty().WithMessage("GameId is required");
    }
}
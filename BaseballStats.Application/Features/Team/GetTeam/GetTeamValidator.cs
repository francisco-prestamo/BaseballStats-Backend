using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.TeamNamespace.GetTeam;

public class GetTeamValidator : Validator<GetTeamCommand>
{
    public GetTeamValidator()
    {
        RuleFor(x => x.TeamId)
            .NotEmpty().WithMessage("TeamId is required")
            .GreaterThan(0).WithMessage("TeamId must be greater than 0");
    }
}
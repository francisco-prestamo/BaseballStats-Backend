using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.DirectionStaffTeam.Delete;

public class DeleteDirectionStaffTeamValidator : Validator<DeleteDirectionStaffTeamCommand>
{
    public DeleteDirectionStaffTeamValidator()
    {
        RuleFor(x => x.TeamId)
            .NotEmpty().WithMessage("TeamId is required.")
            .GreaterThan(0).WithMessage("TeamId must be greater than 0.");

        RuleFor(x => x.DirectionMemberId)
            .NotEmpty().WithMessage("DirectionMemberId is required.")
            .GreaterThan(0).WithMessage("DirectionMemberId must be greater than 0.");
    }
}
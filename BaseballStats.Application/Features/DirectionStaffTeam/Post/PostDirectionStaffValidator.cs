using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.DirectionStaffTeam.Post;

public class PostDirectionStaffTeamValidator : Validator<PostDirectionStaffTeamCommand>
{
    public PostDirectionStaffTeamValidator()
    {
        RuleFor(x => x.TeamId)
            .NotEmpty().WithMessage("TeamId is required.")
            .GreaterThan(0).WithMessage("TeamId must be greater than 0.");

        RuleFor(x => x.DirectionMemberId)
            .NotEmpty().WithMessage("DirectionMemberId is required.")
            .GreaterThan(0).WithMessage("DirectionMemberId must be greater than 0.");
    }
}
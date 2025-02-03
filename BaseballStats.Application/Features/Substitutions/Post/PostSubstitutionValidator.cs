using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Substitutions.Post;

public class PostSubstitutionValidator : Validator<PostSubstitutionCommand>
{
    public PostSubstitutionValidator()
    {
        RuleFor(x => x.GameId)
            .NotEmpty().WithMessage("GameId is required")
            .GreaterThan(0).WithMessage("GameId must be greater than 0");
    
        RuleFor(x => x.TeamId)
            .NotEmpty().WithMessage("TeamId is required")
            .GreaterThan(0).WithMessage("TeamId must be greater than 0");
    
        RuleFor(x => x.PlayerInId)
            .NotEmpty().WithMessage("PlayerInId is required")
            .GreaterThan(0).WithMessage("PlayerInId must be greater than 0");
    
        RuleFor(x => x.PlayerOutId)
            .NotEmpty().WithMessage("PlayerOutId is required")
            .GreaterThan(0).WithMessage("PlayerOutId must be greater than 0");

        RuleFor(x => x.Time)
            .NotEmpty().WithMessage("Time is required");
    }
}
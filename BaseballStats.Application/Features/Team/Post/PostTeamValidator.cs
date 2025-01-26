using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Team.Post;

public class PostTeamValidator : Validator<PostTeamCommand>
{
    public PostTeamValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(255).WithMessage("Name must not exceed 255 characters");

        RuleFor(x => x.Initials)
            .NotEmpty().WithMessage("Initials is required")
            .MaximumLength(3).WithMessage("Initials must not exceed 3 characters");

        RuleFor(x => x.RepresentedEntity)
            .NotEmpty().WithMessage("RepresentedEntity is required")
            .MaximumLength(255).WithMessage("RepresentedEntity must not exceed 255 characters");

        RuleFor(x => x.Color)
            .NotEmpty().WithMessage("Color is required")
            .MaximumLength(50).WithMessage("Color must not exceed 50 characters");

        RuleFor(x => x.DtId)
            .NotEmpty().WithMessage("TechnicalDirector is required");
    }
}
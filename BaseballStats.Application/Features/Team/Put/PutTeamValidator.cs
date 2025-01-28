using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Team.Put;

public class PutTeamValidator : Validator<PutTeamCommand>
{
    public PutTeamValidator()
    {
        RuleFor(x => x.Id).NotEmpty().When(x => x.Id != 0)
            .WithMessage("Id is required");

        RuleFor(x => x.Name).NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.RepresentedEntity).NotEmpty()
            .WithMessage("RepresentedEntity is required");

        RuleFor(x => x.Initials).NotEmpty()
            .WithMessage("Initials is required");

        RuleFor(x => x.Initials).MaximumLength(3)
            .WithMessage("Initials must be 3 characters or less");

        RuleFor(x => x.Color).NotEmpty()
            .WithMessage("Color is required");

        RuleFor(x => x.DtId).NotEmpty().When(x => x.DtId != 0)
            .WithMessage("DtId is required");
    }

}
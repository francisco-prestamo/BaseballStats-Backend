using BaseballStats.Application.Utilities;
using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.PlayerInPosition.Put;

public class PutPlayerInPositionValidator : Validator<PutPlayerInPositionCommand>
{
    public PutPlayerInPositionValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty().When(x => x.PlayerId != 0)
            .WithMessage("PlayerId is required");
        RuleFor(x => x.Position).NotEmpty()
            .WithMessage("Position is required");

        RuleFor(x => x.Position).Must(x => x.IsValidPosition()).
            WithMessage("Invalid position name");

        RuleFor(x => x.Effectiveness).NotEmpty().When(x => x.Effectiveness != 0)
            .WithMessage("Effectiveness is required");
    }

}  

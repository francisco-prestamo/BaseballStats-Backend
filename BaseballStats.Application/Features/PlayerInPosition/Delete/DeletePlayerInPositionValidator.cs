using FastEndpoints;
using FluentValidation;
using BaseballStats.Application.Utilities;

namespace BaseballStats.Application.Features.PlayerInPosition.Delete;

public class DeletePlayerInPositionValidator : Validator<DeletePlayerInPositionCommand>
{
    public DeletePlayerInPositionValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty().When(x => x.PlayerId != 0)
            .WithMessage("PlayerId is required");
        
        RuleFor(x => x.Position).NotEmpty()
            .WithMessage("Position is required");

        RuleFor(x => x.Position).Must(x => x.IsValidPosition())
            .WithMessage("Position is invalid");
    }

}
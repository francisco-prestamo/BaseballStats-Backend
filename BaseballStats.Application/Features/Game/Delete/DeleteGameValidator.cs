using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Game.Delete;

public class DeleteGameValidator : Validator<DeleteGameCommand>
{
    public DeleteGameValidator()
    {
        RuleFor(x => x.GameId)
            .NotEmpty().WithMessage("GameId is required")
            .GreaterThan(0).WithMessage("GameId must be greater than 0");
    }
}
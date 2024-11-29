using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Game.GetGameObject;

public class GetGameObjectValidator : Validator<GetGameObjectCommand>
{
    public GetGameObjectValidator()
    {
        RuleFor(x => x.GameId)
            .NotEmpty().WithMessage("GameId is required")
            .GreaterThan(0).WithMessage("GameId must be greater than 0");
    }
}
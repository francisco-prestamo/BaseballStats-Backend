using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Game.GetSubstitutions;

public class GetSubstitutionsValidator : Validator<GetSubstitutionsCommand>
{
    public GetSubstitutionsValidator()
    {
        RuleFor(x => x.GameId)
            .NotEmpty().WithMessage("GameId is required")
            .GreaterThan(0).WithMessage("GameId must be greater than 0");
    }
}
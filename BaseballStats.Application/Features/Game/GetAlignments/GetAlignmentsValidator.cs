using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Game.GetAlignments;

public class GetAlignmentsValidator : Validator<GetAlignmentsCommand>
{
    public GetAlignmentsValidator()
    {
        RuleFor(x => x.GameId)
            .NotEmpty().WithMessage("GameId is required")
            .GreaterThan(0).WithMessage("GameId must be greater than 0");
    }
}
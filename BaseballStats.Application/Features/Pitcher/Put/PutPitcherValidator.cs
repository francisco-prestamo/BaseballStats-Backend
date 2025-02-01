using System.Data;
using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Pitcher.Put;

public class PutPitcherValidator : Validator<PutPitcherCommand>
{
    public PutPitcherValidator()
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty().When(x => x.PlayerId != 0).WithMessage("PlayerId is required");

        RuleFor(x => x.GamesWonNumber)
            .NotEmpty().When(x => x.GamesWonNumber != 0).WithMessage("GamesWonNumber is required")
            .GreaterThanOrEqualTo(0).WithMessage("GamesWonNumber must be greater than or equal to 0");

        RuleFor(x => x.GamesLostNumber)
            .NotEmpty().When(x => x.GamesLostNumber != 0).WithMessage("GamesLostNumber is required")
            .GreaterThanOrEqualTo(0).WithMessage("GamesLostNumber must be greater than or equal to 0");

        RuleFor(x => x.AllowedRunsAvg)
            .NotEmpty().When(x => x.AllowedRunsAvg != 0).WithMessage("AllowedRunsAvg is required")
            .InclusiveBetween(0, 1).WithMessage("AllowedRunsAvg must be between 0 and 1");

        RuleFor(x => x.Effectiveness)
            .NotEmpty().When(x => x.Effectiveness != 0).WithMessage("Effectiveness is required");
    
        RuleFor(x => x.RightHanded)
            .NotEmpty().WithMessage("RightHanded is required");
    }
}
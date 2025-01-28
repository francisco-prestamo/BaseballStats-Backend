using System.Data;
using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Pitcher.Put;

public class PutPitcherValidator : Validator<PutPitcherCommand>
{
    public PutPitcherValidator()
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty().WithMessage("PlayerId is required")
            .GreaterThan(0).WithMessage("PlayerId must be greater than 0.");

        RuleFor(x => x.GamesWonNumber)
            .NotEmpty().WithMessage("GamesWonNumber is required")
            .GreaterThanOrEqualTo(0).WithMessage("GamesWonNumber must be greater than or equal to 0");

        RuleFor(x => x.GamesLostNumber)
            .NotEmpty().WithMessage("GamesLostNumber is required")
            .GreaterThanOrEqualTo(0).WithMessage("GamesLostNumber must be greater than or equal to 0");

        RuleFor(x => x.AllowedRunsAvg)
            .NotEmpty().WithMessage("AllowedRunsAvg is required")
            .InclusiveBetween(0, 1).WithMessage("AllowedRunsAvg must be between 0 and 1");

        RuleFor(x => x.Effectiveness)
            .NotEmpty().WithMessage("AllowedRunsAvg is required");
    
        RuleFor(x => x.RightHanded)
            .NotEmpty().WithMessage("RightHanded is required");
    }
}
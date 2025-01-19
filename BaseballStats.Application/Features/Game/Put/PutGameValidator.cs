using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Game.Put;

public class PutGameValidator : Validator<PutGameCommand>
{
    public PutGameValidator()
    {
        RuleFor(x => x.Team1Id)
            .NotEmpty().WithMessage("Team1Id is required.")
            .GreaterThan(0).WithMessage("Team1Id must be greater than 0.");

        RuleFor(x => x.Team2Id)
            .NotEmpty().WithMessage("Team2Id is required.")
            .GreaterThan(0).WithMessage("Team2Id must be greater than 0.");

        RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required.");

        RuleFor(x => x.Team1Runs)
            .GreaterThanOrEqualTo(0).WithMessage("Team1Runs must be greater than or equal to 0.");

        RuleFor(x => x.Team2Runs)
            .GreaterThanOrEqualTo(0).WithMessage("Team2Runs must be greater than or equal to 0.");

        RuleFor(x => x.SeriesId)
            .NotEmpty().WithMessage("SeriesId is required.")
            .GreaterThan(0).WithMessage("SeriesId must be greater than 0.");

        RuleFor(x => x.WinTeam).NotNull().WithMessage("WinTeam is required.");

        RuleFor(x => x)
            .Must(ValidateRuns).WithMessage("Runs must be different and the win team must have more runs.");
        
        RuleFor(x => x)
            .Must(x => x.Team1Id != x.Team2Id).WithMessage("Team1Id and Team2Id must be different.");
    }
    
    private static bool ValidateRuns(PutGameCommand command)
    {
        var result = command.Team1Runs != command.Team2Runs;

        if (command.WinTeam)
            result &= command.Team1Runs > command.Team2Runs;
        else
            result &= command.Team1Runs < command.Team2Runs;

        return result;
    }
}
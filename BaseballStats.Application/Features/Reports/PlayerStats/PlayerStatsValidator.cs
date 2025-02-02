using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Reports.PlayerStats;

public class PlayerStatsValidator : Validator<PlayerStatsCommand>
{
    public PlayerStatsValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty().When(x => x.PlayerId != 0)
            .WithMessage("Player Id is required");
    }
}


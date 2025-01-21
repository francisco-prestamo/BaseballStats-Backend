using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Player.GetPlayerOrPitcher;

public class GetPlayerOrPitcherValidator : Validator<GetPlayerOrPitcherCommand>
{
    public GetPlayerOrPitcherValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("PlayerId is required");

    }
}
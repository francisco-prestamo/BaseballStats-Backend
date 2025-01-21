using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Player.GetPlayer;

public class GetPlayerValidator : Validator<GetPlayerCommand>
{
    public GetPlayerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}
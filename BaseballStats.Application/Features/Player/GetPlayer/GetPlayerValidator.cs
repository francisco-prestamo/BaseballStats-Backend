using BaseballStats.Application.Features.Player.GetPlayer;
using FastEndpoints;
using FluentValidation;

namespace BaseballStats.WebApi.Endpoints.Player.GetPlayer;

public class GetPlayerValidator : Validator<GetPlayerCommand>
{
    public GetPlayerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}
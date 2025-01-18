using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Player.Delete;

public class DeletePlayerValidator : Validator<DeletePlayerCommand>
{
    public DeletePlayerValidator()
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty().WithMessage("PlayerId is required");
    }
}
using System.Data;
using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Pitcher.Delete;

public class DeletePitcherValidator : Validator<DeletePitcherCommand>
{
    public DeletePitcherValidator()
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty().When(x => x.PlayerId != 0).WithMessage("PlayerId is required")
            .GreaterThan(0).WithMessage("PlayerId must be greater than 0.");
    }
}
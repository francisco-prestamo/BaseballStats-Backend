using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Player.Create;

public class CreatePlayerValidator : Validator<CreatePlayerCommand>
{
    public CreatePlayerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Age)
            .NotEmpty().WithMessage("Age is required")
            .GreaterThan(0).WithMessage("Age must be greater than 0");

        RuleFor(x => x.YearsOfExperience)
            .NotEmpty().WithMessage("YearsOfExperience is required")
            .GreaterThan(0).WithMessage("YearsOfExperience must be greater than 0");

        RuleFor(x => x.BattingAverage)
            .InclusiveBetween(0, 1).When(x => x.BattingAverage != null)
            .WithMessage("BattingAverage must be between 0 and 1");
    }
}
using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.DirectionStaff.Delete;

public class DeleteDirectionStaffValidator : Validator<DeleteDirectionStaffCommand>
{
    public DeleteDirectionStaffValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}
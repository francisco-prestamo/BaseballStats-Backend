using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.DirectionStaff.Put;

public class PutDirectionStaffValidator : Validator<PutDirectionStaffCommand>
{
    public PutDirectionStaffValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
    }
}
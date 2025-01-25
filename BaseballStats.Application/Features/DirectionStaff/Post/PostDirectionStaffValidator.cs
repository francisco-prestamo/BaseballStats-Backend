using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.DirectionStaff.Post;

public class PostDirectionStaffValidator : Validator<PostDirectionStaffCommand>
{
    public PostDirectionStaffValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
    }
}
using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Season.Post;

public class PostSeasonValidator : Validator<PostSeasonCommand>
{
    public PostSeasonValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required")
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}
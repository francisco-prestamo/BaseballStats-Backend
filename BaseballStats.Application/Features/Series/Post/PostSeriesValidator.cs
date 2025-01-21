using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Series.Post;

public class PostSeriesValidator : Validator<PostSeriesCommand>
{
    public PostSeriesValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        
        RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required.");
        
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start Date is required.");
        
        RuleFor(x => x.EndDate).NotEmpty().WithMessage("End Date is required.");
        
        RuleFor(x => x.SeasonId)
            .NotEmpty().WithMessage("Season Id is required.")
            .GreaterThan(0).WithMessage("Season Id must be greater than 0.");
    }
}
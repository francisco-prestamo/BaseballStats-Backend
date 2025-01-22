using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Series.Get;

public class GetSerieValidator : Validator<GetSerieCommand>
{
    public GetSerieValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}
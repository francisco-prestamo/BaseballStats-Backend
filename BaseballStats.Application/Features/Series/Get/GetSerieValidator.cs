using FastEndpoints;
using FluentValidation;

namespace BaseballStats.Application.Features.Series.Get;

public class GetSerieValidator : Validator<GetSerieCommand>
{
    public GetSerieValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
            .GreaterThan(0).WithMessage("Id must be greater than 0");

        RuleFor(x => x.SeasonId).NotEmpty().WithMessage("SeasonId is required")
            .GreaterThan(0).WithMessage("SeasonId must be greater than 0");
    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Team.GetAll;
using BaseballStats.Application.Features.TechnicalDirector.GetAll;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.TechnicalDirector;

public class GetAllTechnicalDirectorsEndpoint : EndpointWithoutRequest<List<RegisteredUserDto>>
{
    public override void Configure()
    {
        Get("/technicalDirectors");
        Roles("Admin");
        Summary(x => x.Summary = "Get all technical directors");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetAllTechnicalDirectorsCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
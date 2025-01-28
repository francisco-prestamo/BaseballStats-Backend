using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Team.GetAll;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Team;

public class GetAllTeamsEndpoint : EndpointWithoutRequest<List<TeamDto>>
{
    public override void Configure()
    {
        Get("/teams");
        Roles("Journalist", "TechnicalDirector", "Admin");
        Summary(x => x.Summary = "Gets all teams");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetAllTeamsCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
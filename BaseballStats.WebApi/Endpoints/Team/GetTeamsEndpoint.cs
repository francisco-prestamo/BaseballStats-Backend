using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Team.GetTeams;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Team;

public class GetTeamsEndpoint : EndpointWithoutRequest<List<TeamAdminDto>>
{
    public override void Configure()
    {
        Get("teams/");
        Roles("Admin");
        Summary(x => x.Summary = "Obtiene todos los equipos");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetTeamsCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
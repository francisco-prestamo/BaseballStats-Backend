using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Team.Put;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Team;

public class PutTeamEndpoint : Endpoint<PutTeamCommand, TeamDto>
{
    public override void Configure()
    {
        Put("/teams/{Id}");
        Roles("Admin");
        Summary(x => x.Summary = "Updates the information of a team");
    }

    public override async Task HandleAsync(PutTeamCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
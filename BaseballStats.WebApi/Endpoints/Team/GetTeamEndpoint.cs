using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.TeamNamespace.GetTeam;

namespace BaseballStats.WebApi.Endpoints.Team;

public class GetTeamEndpoint : Endpoint<GetTeamCommand, TeamDto>
{
    public override void Configure()
    {
        Get("teams/${teamId}");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene la información de un equipo que participa en una serie");
    }

    public override async Task HandleAsync(GetTeamCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
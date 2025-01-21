using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.TeamNamespace.GetTeamStarPlayers;

namespace BaseballStats.WebApi.Endpoints.Team;

public class GetTeamStarPlayersEndpoint : Endpoint<GetTeamStarPlayersCommand, List<PlayerInPositionDto>>
{
    public override void Configure()
    {
        Get("teams/${teamId}/serie/${seasonId}/${seriesId}/star-players");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene los jugadores estrellas de un equipo que participa en una serie");
    }

    public override async Task HandleAsync(GetTeamStarPlayersCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
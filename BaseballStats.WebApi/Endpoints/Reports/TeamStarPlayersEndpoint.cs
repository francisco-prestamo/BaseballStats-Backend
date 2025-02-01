using BaseballStats.Application.Features.Reports.TeamStarPlayers;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Reports;

public class TeamStarPlayersEndpoint : Endpoint<TeamStarPlayersCommand, FileInfo>
{
    public override void Configure()
    {
        Get("reports/teams/{teamId}/serie/{seasonId}/{seriesId}/star-players");
        Summary(x => x.Summary = "Genera un reporte de los jugadores estrellas de un equipo que participa en una serie");
    }
    
    public override async Task HandleAsync(TeamStarPlayersCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendFileAsync(response, "application/pdf", cancellation: ct);
    }
}
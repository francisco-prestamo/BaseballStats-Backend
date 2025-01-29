using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.PlayerInPosition.GetTeamInSeriesPlayerInPositions;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.PlayerInPosition;

public class GetTeamInSeriesPlayerInPositionsEndpoint : Endpoint<GetTeamInSeriesPlayerInPositionsCommand, List<PlayerInPositionCRUDDto>>
{
    public override void Configure()
    {
        Get("playerInPositions/series/{seasonId}/{seriesId}/team/{teamId}");
        Roles("Admin", "Journalist", "TechnicalDirector");
        Summary(x => x.Summary = "For a given team in a series, returns the players and their playabale positions");
    }

    public override async Task HandleAsync(GetTeamInSeriesPlayerInPositionsCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
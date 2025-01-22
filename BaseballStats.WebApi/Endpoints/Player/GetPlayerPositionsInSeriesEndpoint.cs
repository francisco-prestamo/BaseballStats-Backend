using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Player.GetPlayerPositionsInSeries;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Player;

public class GetPlayerPositionsInSeriesEndpoint : Endpoint<GetPlayerPositionsInSeriesCommand, List<PlayerInPositionDto>>
{
    public override void Configure()
    {
        Get("players/{PlayerId}/season/{SeasonId}/series/{SeriesId}/positions");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene las posiciones de un jugador en una serie");
    }

    public override async Task HandleAsync(GetPlayerPositionsInSeriesCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
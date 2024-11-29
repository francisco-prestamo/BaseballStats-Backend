using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Game.GetGamesFromSeries;

namespace BaseballStats.WebApi.Endpoints.Game;

public class GetGamesFromSeriesEndpoint : Endpoint<GetGamesFromSeriesCommand, List<GameDto>>
{
    public override void Configure()
    {
        Get("series/{SeasonId}/{SeriesId}/games");
        Summary(x => x.Summary = "Obtiene todos los juegos de una serie");
    }

    public override async Task HandleAsync(GetGamesFromSeriesCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
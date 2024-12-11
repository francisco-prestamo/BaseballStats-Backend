using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Series.GetTeamsFromSeries;

namespace BaseballStats.WebApi.Endpoints.Series;

public class GetTeamsFromSeriesEndpoint : Endpoint<GetTeamsFromSeriesCommand, List<TeamWithExtrasDto>>
{
    public override void Configure()
    {
        Get("series/{SeasonId}/{SeriesId}/teams");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene todos los equipos con al menos un jugador en una serie");
    }

    public override async Task HandleAsync(GetTeamsFromSeriesCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
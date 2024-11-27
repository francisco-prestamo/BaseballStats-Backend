using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Season.GetSeriesFromSeason;

namespace BaseballStats.WebApi.Endpoints.Season;

public class GetSeriesFromSeasonEndpoint : Endpoint<GetSeriesFromSeasonCommand, List<SeriesDto>>
{
    public override void Configure()
    {
        Get("series/{SeasonId}/");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene todas las series de una temporada");

        /* Cuando haya que restringir acceso con roles
         * Roles(["Admin", "Journalist", "TechnicalDirector"]);
         */
    }

    public override async Task HandleAsync(GetSeriesFromSeasonCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
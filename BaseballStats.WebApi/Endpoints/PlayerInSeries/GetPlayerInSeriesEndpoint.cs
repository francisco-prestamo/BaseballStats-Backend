using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.PlayerInSeries.Delete;
using BaseballStats.Application.Features.PlayerInSeries.Get;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.PlayerInSeries;

public class GetPlayerInSeriesEndpoint : Endpoint<GetPlayerInSeriesCommand, PlayerInSeriesCRUDDto>
{
    public override void Configure()
    {
        Get("/playerInSeries/{PlayerId}/{SeasonId}/{SerieId}");
        Roles("Journalist", "TechnicalDirector", "Admin");
        Summary(x => x.Summary = "Gets a player's team assignment for a series");
    }

    public override async Task HandleAsync(GetPlayerInSeriesCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
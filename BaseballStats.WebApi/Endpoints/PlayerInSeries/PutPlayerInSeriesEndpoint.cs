using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.PlayerInSeries.Put;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.PlayerInSeries;

public class PutPlayerInSeriesEndpoint : Endpoint<PutPlayerInSeriesCommand, PlayerInSeriesCRUDDto>
{
    public override void Configure()
    {
        Put("/playerInSeries/{PlayerId}/{SeasonId}/{SerieId}");
        Roles("Admin");
        Summary(x => x.Summary = "Updates the team assigned to a player in a series");
    }

    public override async Task HandleAsync(PutPlayerInSeriesCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
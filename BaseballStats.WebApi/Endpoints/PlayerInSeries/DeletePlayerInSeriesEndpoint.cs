using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Player.Delete;
using BaseballStats.Application.Features.PlayerInSeries.Delete;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.PlayerInSeries;

public class DeletePlayerInSeriesEndpoint : Endpoint<DeletePlayerInSeriesCommand, PlayerInSeriesCRUDDto>
{
    public override void Configure()
    {
        Delete("/playerInSeries/{PlayerId}/{SeasonId}/{SerieId}");
        Roles("Admin");
        Summary(x => x.Summary = "Deletes a player's team assignment for a series");
    }

    public override async Task HandleAsync(DeletePlayerInSeriesCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
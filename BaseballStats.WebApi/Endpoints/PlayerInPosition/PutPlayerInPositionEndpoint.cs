using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.PlayerInPosition.Put;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.PlayerInPosition;

public class PutPlayerInPositionEndpoint : Endpoint<PutPlayerInPositionCommand, PlayerInPositionCRUDDto>
{
    public override void Configure()
    {
        Put("/playerInPositions/{PlayerId}/{Position}");
        Roles("Admin");
        Summary(x => x.Summary = "Updates the effectiveness of a player in a given position");
    }

    public override async Task HandleAsync(PutPlayerInPositionCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
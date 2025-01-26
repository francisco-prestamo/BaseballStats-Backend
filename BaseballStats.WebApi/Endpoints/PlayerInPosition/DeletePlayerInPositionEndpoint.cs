using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Player.Delete;
using BaseballStats.Application.Features.PlayerInPosition.Delete;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.PlayerInPosition;

class DeletePlayerInPositionEndpoint : Endpoint<DeletePlayerInPositionCommand, PlayerInPositionCRUDDto>
{
    public override void Configure()
    {
        Delete("/playerInPositions/{PlayerId}/{Position}");
        Roles("Admin");
        Summary(x => x.Summary = "Deletes a player available position assignment");
    }

    public override async Task HandleAsync(DeletePlayerInPositionCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }

}
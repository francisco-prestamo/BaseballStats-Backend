using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Player.GetPlayer;
using BaseballStats.Application.Features.PlayerInPosition.Get;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.PlayerInPosition;

public class GetPlayerInPositionEndpoint : Endpoint<GetPlayerInPositionCommand, PlayerInPositionCRUDDto>
{
    public override void Configure()
    {
        Get("/playerInPositions/{PlayerId}/{Position}");
        Roles("Journalist", "TechnicalDirector", "Admin");
        Summary(x => x.Summary = "Gets a player available position assignment");
    }

    public override async Task HandleAsync(GetPlayerInPositionCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
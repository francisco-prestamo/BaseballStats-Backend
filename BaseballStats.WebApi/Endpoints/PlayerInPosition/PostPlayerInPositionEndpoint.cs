using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.PlayerInPosition.Post;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.PlayerInPosition;

public class PostPlayerInPositionEndpoint : Endpoint<PostPlayerInPositionCommand, PlayerInPositionCRUDDto>
{
    public override void Configure()
    {
        Post("/playerInPositions");
        Roles("Admin");
        Summary(x => x.Summary = "Assigns to a player a new position that can play");
    }

    public override async Task HandleAsync(PostPlayerInPositionCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status201Created, ct);
    }
}
using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Player.Post;

namespace BaseballStats.WebApi.Endpoints.Player;

public class PostPlayerEndpoint : Endpoint<PostPlayerCommand, RegularPlayerDto>
{
    public override void Configure()
    {
        Post("players");
        Roles("Admin");
        Summary(x => x.Summary = "Crea un nuevo jugador");
    }

    public override async Task HandleAsync(PostPlayerCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status201Created, ct);
    }
}


using BaseballStats.Application.Features.Game.Post;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Game;

public class PostGamesEndpoint : Endpoint<PostGameCommand, PostGameResponse>
{
    public override void Configure()
    {
        Post("games");
        AllowAnonymous();
        Summary(x => x.Summary = "Add a game to a series");
    }

    public override async Task HandleAsync(PostGameCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, cancellation: ct);
    }
}
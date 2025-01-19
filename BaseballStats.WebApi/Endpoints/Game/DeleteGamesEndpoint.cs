using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Game.Delete;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Game;

public class DeleteGamesEndpoint : Endpoint<DeleteGameCommand, GameDto>
{
    public override void Configure()
    {
        Delete("/games/{GameId}");
        AllowAnonymous();
        Summary(x => x.Summary = "Delete a game");
    }

    public override async Task HandleAsync(DeleteGameCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
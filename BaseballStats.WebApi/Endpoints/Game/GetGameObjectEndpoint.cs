using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Game.GetGameObject;

namespace BaseballStats.WebApi.Endpoints.Game;

public class GetGameObjectEndpoint : Endpoint<GetGameObjectCommand, GameDto>
{
    public override void Configure()
    {
        Get("games/{GameId}");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene el juego cuyo id es gameId");
    }

    public override async Task HandleAsync(GetGameObjectCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
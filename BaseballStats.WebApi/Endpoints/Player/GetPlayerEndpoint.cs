using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Player.GetPlayer;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Player;

public class GetPlayerEndpoint : Endpoint<GetPlayerCommand, RegularPlayerDto>
{
    public override void Configure()
    {
        Get("players/{id}");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene un jugador por su Id");
    }

    public override async Task HandleAsync(GetPlayerCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Player.GetPlayer;

namespace BaseballStats.WebApi.Endpoints.Player;

public class GetPlayerEndpoint : Endpoint<GetPlayerCommand, RegularPlayerDto>
{
    public override void Configure()
    {
        Get("players/{Id}");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene un jugador por Id");
    }

    public override async Task HandleAsync(GetPlayerCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
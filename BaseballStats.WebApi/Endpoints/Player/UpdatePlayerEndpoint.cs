using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Player.Put;

namespace BaseballStats.Application.Features.Player.Put;

public class UpdatePlayerEndpoint : Endpoint<UpdatePlayerCommand, RegularPlayerDto>
{
    public override void Configure()
    {
        Put("players/{Id}");
        Roles("Admin");
        Summary(x => x.Summary = "Actualiza un jugador");
    }

    public override async Task HandleAsync(UpdatePlayerCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Player.Put;

namespace BaseballStats.Application.Features.Player.Put;

public class PutPlayerEndpoint : Endpoint<PutPlayerCommand, RegularPlayerDto>
{
    public override void Configure()
    {
        Put("players/{Id}");
        Roles("Admin");
        Summary(x => x.Summary = "Updates a player");
    }

    public override async Task HandleAsync(PutPlayerCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
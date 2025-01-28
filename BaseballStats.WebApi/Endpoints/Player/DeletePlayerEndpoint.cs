using FastEndpoints;
using BaseballStats.Application.Features.Player.Delete;

namespace BaseballStats.WebApi.Endpoints.Player;

public class DeletePlayerEndpoint : Endpoint<DeletePlayerCommand, EmptyResponse>
{
    public override void Configure()
    {
        Delete("players/{PlayerId}");
        Roles("Admin");
        Summary(x => x.Summary = "Elimina un jugador");
    }

    public override async Task HandleAsync(DeletePlayerCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status204NoContent, ct);
    }
}
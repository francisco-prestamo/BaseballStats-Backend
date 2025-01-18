using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Player.Create;

namespace BaseballStats.WebApi.Endpoints.Player;

public class CreatePlayerEndpoint : Endpoint<CreatePlayerCommand, RegularPlayerDto>
{
    public override void Configure()
    {
        Post("players");
        // Roles("Admin");
        AllowAnonymous();
        Summary(x => x.Summary = "Crea un nuevo jugador");
    }

    public override async Task HandleAsync(CreatePlayerCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status201Created, ct);
    }
}


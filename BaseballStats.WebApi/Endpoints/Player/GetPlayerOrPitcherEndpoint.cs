using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Player.GetPlayerOrPitcher;


namespace BaseballStats.WebApi.Endpoints.Player;

public class GetPlayerOrPitcherEndpoint : Endpoint<GetPlayerOrPitcherCommand, PlayerOrPitcherDto>
{
    public override void Configure()
    {
        Get("players/orPitchers/{Id}");
        Roles("Journalist", "TechnicalDirector", "Admin");
        Summary(x => x.Summary = "Obtiene un jugador o pitcher por Id");
    }

    public override async Task HandleAsync(GetPlayerOrPitcherCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
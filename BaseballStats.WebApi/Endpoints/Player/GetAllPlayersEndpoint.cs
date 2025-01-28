using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Player.GetAll;

namespace BaseballStats.WebApi.Endpoints.Player;

public class GetAllPlayersEndpoint : EndpointWithoutRequest<List<RegularPlayerDto>>
{
    public override void Configure()
    {
        Get("players");
        Roles("Journalist", "TechnicalDirector", "Admin");
        Summary(x => x.Summary = "Obtiene todos los jugadores");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetAllPlayersCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
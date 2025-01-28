using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Team.GetTeamPlayersInASerie;

namespace BaseballStats.WebApi.Endpoints.Team;

public class GetTeamPlayersInASerieEndpoint : Endpoint<GetTeamPlayersInASerieCommand, List<RegularPlayerDto>>
{
    public override void Configure()
    {
        Get("teams/{teamId}/serie/{seasonId}/{seriesId}/players");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene los juegos de un equipo en una serie");
    }

    public override async Task HandleAsync(GetTeamPlayersInASerieCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.TeamNamespace.GetTeamGamesInThisSeries;

namespace BaseballStats.WebApi.Endpoints.Team;

public class GetTeamGamesInThisSeriesEndpoint : Endpoint<GetTeamGamesInThisSeriesCommand, List<GameWithTeamsDto>>
{
    public override void Configure()
    {
        Get("teams/{teamId}/serie/{seasonId}/{seriesId}/games");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene los juegos de un equipo en una serie");
    }

    public override async Task HandleAsync(GetTeamGamesInThisSeriesCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
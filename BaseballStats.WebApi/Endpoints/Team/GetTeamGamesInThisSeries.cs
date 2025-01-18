using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Team.GetTeamGamesInThisSeries;

namespace BaseballStats.WebApi.Endpoints.Team;

public class GetTeamGamesInThisSeriesEndpoint : Endpoint<GetTeamGamesInThisSeriesCommand, List<GameDto>>
{
    public override void Configure()
    {
        Get("teams/${seasonId}/${seriesId}/${teamId}/games");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene los juegos de un equipo en una serie");
    }

    public override async Task HandleAsync(GetTeamGamesInThisSeriesCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
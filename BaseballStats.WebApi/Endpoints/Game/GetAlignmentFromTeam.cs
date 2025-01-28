using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Game.GetAlignmentFromTeam;

namespace BaseballStats.WebApi.Endpoints.Game;

public class GetAlignmentFromTeamEndpoint : Endpoint<GetAlignmentFromTeamCommand, List<PlayerInPositionDto>>
{
    public override void Configure()
    {
        Get("games/{GameId}/alignments/{TeamId}");
        Roles("Journalist", "TechnicalDirector", "Admin");
        Summary(x => x.Summary = "Obtiene la alineación de un equipo dado en un juego dado");
    }

    public override async Task HandleAsync(GetAlignmentFromTeamCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
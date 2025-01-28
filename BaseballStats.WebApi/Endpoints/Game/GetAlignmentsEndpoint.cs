using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Game.GetAlignments;

namespace BaseballStats.WebApi.Endpoints.Game;

public class GetAlignmentsEndpoint : Endpoint<GetAlignmentsCommand, AlignmentsDto>
{
    public override void Configure()
    {
        Get("games/{GameId}/alignments");
        Roles("Journalist", "TechnicalDirector", "Admin");
        Summary(x => x.Summary = "Obtiene las alineaciones de los equipos en el juego GameId");
    }

    public override async Task HandleAsync(GetAlignmentsCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
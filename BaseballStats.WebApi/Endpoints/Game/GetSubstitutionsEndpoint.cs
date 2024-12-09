using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Game.GetSubstitutions;

namespace BaseballStats.WebApi.Endpoints.Game;

public class GetSubstitutionsEndpoint : Endpoint<GetSubstitutionsCommand, SubstitutionsDto>
{
    public override void Configure()
    {
        Get("games/{GameId}/substitutions");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene los cambios en las alineaciones de los equipos en el juego GameId");
    }

    public override async Task HandleAsync(GetSubstitutionsCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
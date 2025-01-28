using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Game.Put;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Game;

public class PutGameEndpoint : Endpoint<PutGameCommand, GameDto>
{
    public override void Configure()
    {
        Put("/games/{Id}");
        Roles("Admin");
        Summary(x => x.Summary = "Update a game");
    }

    public override async Task HandleAsync(PutGameCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
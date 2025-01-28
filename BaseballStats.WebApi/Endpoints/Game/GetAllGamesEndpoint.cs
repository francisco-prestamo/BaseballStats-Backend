using FastEndpoints;
using BaseballStats.Application.Features.Game.GetAll;
using BaseballStats.Application.DTOs;

namespace BaseballStats.WebApi.Endpoints.Game;

public class GetAllGamesEndpoint : EndpointWithoutRequest<List<GameDto>>
{
    public override void Configure()
    {
        Get("/games");
        Roles("Journalist", "TechnicalDirector", "Admin");
        Summary(x => x.Summary = "Get all games");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetAllGamesCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
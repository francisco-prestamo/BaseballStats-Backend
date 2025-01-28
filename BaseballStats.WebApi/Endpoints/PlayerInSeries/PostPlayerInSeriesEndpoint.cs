using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.PlayerInSeries.Post;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.PlayerInSeries;

public class PostPlayerInSeriesEndpoint : Endpoint<PostPlayerInSeriesCommand, PlayerInSeriesCRUDDto>
{
    public override void Configure()
    {
        Post("/playerInSeries");
        Roles("Admin");
        Summary(x => x.Summary = "Assigns a player to a team in a series");
    }

    public override async Task HandleAsync(PostPlayerInSeriesCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status201Created, ct);
    }
}
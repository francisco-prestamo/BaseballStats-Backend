using FastEndpoints;
using BaseballStats.Application.Features.StarPlayerInPosition.Post;
using BaseballStats.Application.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BaseballStats.WebApi.Endpoints.StarPlayerInPosition;

public class PostStarPlayerInPositionEndpoint : Endpoint<PostStarPlayerInPositionCommand, StarPlayerInPositionDto>

{
    public override void Configure()
    {
       Post("starPlayerInPosition");
       Roles("Admin");
       Summary(x => x.Summary = "Make a player a star player in a position for a given series");
    }

    public override async Task HandleAsync(PostStarPlayerInPositionCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}

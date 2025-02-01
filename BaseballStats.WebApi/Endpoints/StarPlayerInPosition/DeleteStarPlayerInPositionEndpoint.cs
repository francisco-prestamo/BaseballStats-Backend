using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.DirectionStaff.Delete;
using BaseballStats.Application.Features.StarPlayerInPosition.Delete;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.StarPlayerInPosition;

public class DeleteStarPlayerInPositionEndpoint : Endpoint<DeleteStarPlayerInPositionCommand, StarPlayerInPositionDto>
{
    public override void Configure()
    {
        Delete("starPlayerInPosition");
        Roles("Admin");
        Summary(x => x.Summary = "Remove a player as a star player in a position for a given series");
    }

    public override async Task HandleAsync(DeleteStarPlayerInPositionCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}

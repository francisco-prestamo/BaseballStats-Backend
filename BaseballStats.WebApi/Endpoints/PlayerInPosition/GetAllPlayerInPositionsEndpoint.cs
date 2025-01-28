using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.PlayerInPosition.GetAll;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.PlayerInPosition;

public class GetAllPlayerInPositionsEndpoint : EndpointWithoutRequest<List<PlayerInPositionCRUDDto>>
{
    public override void Configure()
    {
        Get("/playerInPositions");
        Roles("Journalist", "TechnicalDirector", "Admin");
        Summary(x => x.Summary = "Gets all player available positions");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetAllPlayerInPositionsCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
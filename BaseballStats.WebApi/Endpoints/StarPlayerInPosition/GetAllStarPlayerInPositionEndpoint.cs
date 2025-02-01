using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.StarPlayerInPosition.GetAll;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.StarPlayerInPosition;

public class GetAllStarPlayerInPositionEndpoint : EndpointWithoutRequest<List<StarPlayerInPositionDto>>
{
    public override void Configure()
    {
        Get("starPlayerInPosition");
        Roles("Journalist", "TechnicalDirector", "Admin");
        Summary(x => x.Summary = "Get all star players with their positions");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetAllStarPlayerInPositionCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
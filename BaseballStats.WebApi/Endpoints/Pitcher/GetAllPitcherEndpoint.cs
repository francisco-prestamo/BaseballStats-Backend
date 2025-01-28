using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Pitcher.GetAll;

namespace BaseballStats.WebApi.Endpoints.Pitcher;

public class GetAllPitchersEndpoint : EndpointWithoutRequest<List<PitcherDto>>
{
    public override void Configure()
    {
        Get("pitchers");
        AllowAnonymous();
        Summary(x => x.Summary = "Gets all pitchers");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetAllPitchersCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
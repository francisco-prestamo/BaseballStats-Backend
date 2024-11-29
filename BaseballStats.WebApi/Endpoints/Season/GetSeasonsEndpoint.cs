using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Season.GetSeasons;

namespace BaseballStats.WebApi.Endpoints.Season;

public class GetSeasonsEndpoint : EndpointWithoutRequest<List<SeasonDto>>
{
    public override void Configure()
    {
        Get("seasons/");
        Summary(x => x.Summary = "Obtiene todas las temporadas");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetSeasonsCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
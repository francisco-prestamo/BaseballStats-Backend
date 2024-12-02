using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Series.GetSeries;

namespace BaseballStats.WebApi.Endpoints.Series;

public class GetSeriesEndpoint : EndpointWithoutRequest<List<SeriesDto>>
{
    public override void Configure()
    {
        Get("series/");
        AllowAnonymous();
        Summary(x => x.Summary = "Obtiene todas las series");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetSeriesCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
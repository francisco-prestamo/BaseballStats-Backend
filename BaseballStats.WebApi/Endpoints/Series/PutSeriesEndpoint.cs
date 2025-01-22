using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Series.Put;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Series;

public class PutSeriesEndpoint : Endpoint<PutSeriesCommand, SeriesDto>
{
    public override void Configure()
    {
        Put("series/{Id}");
        AllowAnonymous();
        Summary(x => x.Summary = "Update a series");
    }

    public override async Task HandleAsync(PutSeriesCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, 200, ct);
    }
}
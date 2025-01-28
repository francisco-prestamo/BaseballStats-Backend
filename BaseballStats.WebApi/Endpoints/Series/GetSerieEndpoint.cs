using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Series.Get;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Series;

public class GetSerieEndpoint : Endpoint<GetSerieCommand, SeriesDto>
{
    public override void Configure()
    {
        Get("series/{SeasonId}/{Id}");
        Roles("Journalist", "TechnicalDirector", "Admin");
        Summary(x => x.Summary = "Get a series by Id");
    }

    public override async Task HandleAsync(GetSerieCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, 200, ct);
    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Series.Post;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Series;

public class PostSeriesEndpoint : Endpoint<PostSeriesCommand, SeriesDto>
{
    public override void Configure()
    {
        Post("series/{SeasonId}");
        AllowAnonymous();
        Summary(x => x.Summary = "Create a new series");
    }

    public override async Task HandleAsync(PostSeriesCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, 200, ct);
    }
}
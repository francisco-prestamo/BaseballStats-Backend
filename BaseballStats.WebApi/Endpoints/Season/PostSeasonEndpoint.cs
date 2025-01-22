using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Season.Post;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Season;

public class PostSeasonEndpoint : EndpointWithoutRequest<SeasonDto>
{
    public override void Configure()
    {
        Post("seasons/");
        AllowAnonymous();
        Summary(x => x.Summary = "Create a new season");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new PostSeasonCommand().ExecuteAsync(ct);
        await SendAsync(response, 200, ct);
    }
}
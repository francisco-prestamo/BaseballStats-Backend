using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Season.Post;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Season;

public class PostSeasonEndpoint : Endpoint<PostSeasonCommand, SeasonDto>
{
    public override void Configure()
    {
        Post("seasons/");
        Roles("Admin");
        Summary(x => x.Summary = "Create a new season");
    }

    public override async Task HandleAsync(PostSeasonCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status201Created, ct);
    }
}
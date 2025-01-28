using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Pitcher.Post;

namespace BaseballStats.WebApi.Endpoints.Pitcher;

public class PostPitcherEndpoint : Endpoint<PostPitcherCommand, PitcherDto>
{
    public override void Configure()
    {
        Post("pitchers");
        Roles("Admin");
        Summary(x => x.Summary = "Add a new pitcher");
    }

    public override async Task HandleAsync(PostPitcherCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status201Created, ct);
    }
}


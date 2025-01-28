using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Team.Post;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Team;

public class PostTeamEndpoint : Endpoint<PostTeamCommand, TeamDto>
{
    public override void Configure()
    {
        Post("/teams");
        Roles("Admin");
        Summary(x => x.Summary = "Creates a new team");
    }

    public override async Task HandleAsync(PostTeamCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status201Created, ct);
    }
}
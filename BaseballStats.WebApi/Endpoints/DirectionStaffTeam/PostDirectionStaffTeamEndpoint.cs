using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.DirectionStaffTeam.Post;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.DirectionStaffTeam;

public class PostDirectionStaffTeamEndpoint : Endpoint<PostDirectionStaffTeamCommand, DirectionStaffTeamDto>
{
    public override void Configure()
    {
        Post("direct");
        AllowAnonymous();
        Summary(x => x.Summary = "Add a DirectionStaff-Team relation");
    }

    public override async Task HandleAsync(PostDirectionStaffTeamCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, cancellation: ct);
    }
}
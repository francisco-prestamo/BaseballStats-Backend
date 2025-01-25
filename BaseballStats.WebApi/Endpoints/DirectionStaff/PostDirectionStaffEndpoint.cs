using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.DirectionStaff.Post;

namespace BaseballStats.WebApi.Endpoints.DirectionStaff;

public class PostDirectionStaffEndpoint : Endpoint<PostDirectionStaffCommand, DirectionStaffDto>
{
    public override void Configure()
    {
        Post("DirectionMembers");
        AllowAnonymous();
        Summary(x => x.Summary = "Create a Direction Staff");
    }

    public override async Task HandleAsync(PostDirectionStaffCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status201Created, ct);
    }
}


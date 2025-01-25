using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.DirectionStaff.Delete;

namespace BaseballStats.WebApi.Endpoints.DirectionStaff;

public class DeleteDirectionStaffEndpoint : Endpoint<DeleteDirectionStaffCommand, DirectionStaffDto>
{
    public override void Configure()
    {
        Delete("DirectionMembers/{Id}");
        AllowAnonymous();
        Summary(x => x.Summary = "Delete a Direction Staff");
    }

    public override async Task HandleAsync(DeleteDirectionStaffCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}


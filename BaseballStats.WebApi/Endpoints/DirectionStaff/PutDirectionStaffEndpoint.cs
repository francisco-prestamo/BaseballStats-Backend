using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.DirectionStaff.Put;

namespace BaseballStats.WebApi.Endpoints.DirectionStaff;

public class PutDirectionStaffEndpoint : Endpoint<PutDirectionStaffCommand, DirectionStaffDto>
{
    public override void Configure()
    {
        Put("DirectionMembers/{Id}");
        AllowAnonymous();
        Summary(x => x.Summary = "Update a Direction Staff");
    }

    public override async Task HandleAsync(PutDirectionStaffCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}


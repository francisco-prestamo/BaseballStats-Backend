using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.DirectionStaffTeamNamespace.Delete;

namespace BaseballStats.WebApi.Endpoints.DirectionStaffTeam;

public class DeleteDirectionStaffTeamEndpoint : Endpoint<DeleteDirectionStaffTeamCommand, DirectionStaffTeamDto>
{
    public override void Configure()
    {
        Delete("direct/{TeamId}/{DirectionMemberId}");
        AllowAnonymous();
        Summary(x => x.Summary = "Delete a DirectionStaff-Team relation");
    }

    public override async Task HandleAsync(DeleteDirectionStaffTeamCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
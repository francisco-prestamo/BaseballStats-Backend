using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Team.Delete;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Team;

public class DeleteTeamEndpoint : Endpoint<DeleteTeamCommand, TeamDto>
{
    public override void Configure()
    {
        Delete("/teams/{Id}");
        Roles("Admin");
        Summary(x => x.Summary = "Deletes a team");
    }

    public override async Task HandleAsync(DeleteTeamCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
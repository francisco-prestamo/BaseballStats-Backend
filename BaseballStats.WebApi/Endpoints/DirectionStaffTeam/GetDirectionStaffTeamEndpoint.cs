using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.DirectionStaffTeamNamespace.Get;

namespace BaseballStats.WebApi.Endpoints.DirectionStaffTeam;

public class GetDirectionStaffTeamEndpoint : EndpointWithoutRequest<List<DirectionStaffTeamDto>>
{
    public override void Configure()
    {
        Get("direct");
        AllowAnonymous();
        Summary(x => x.Summary = "Get All DirectionStaff-Team");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetDirectionStaffTeamCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
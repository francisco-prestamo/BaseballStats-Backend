using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.DirectionStaff.GetAll;

namespace BaseballStats.WebApi.Endpoints.DirectionStaff;

public class GetAllDirectionStaffEndpoint : EndpointWithoutRequest<List<DirectionStaffDto>>
{
    public override void Configure()
    {
        Get("direct");
        AllowAnonymous();
        Summary(x => x.Summary = "Get all Direction Staff Members");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetAllDirectionStaffCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}


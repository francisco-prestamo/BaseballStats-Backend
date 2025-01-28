using FastEndpoints;
using BaseballStats.Application.Features.Pitcher.Delete;
using BaseballStats.Application.DTOs;

namespace BaseballStats.WebApi.Endpoints.Pitcher;

public class DeletePitcherEndpoint : Endpoint<DeletePitcherCommand, PitcherDto>
{
    public override void Configure()
    {
        Delete("pitchers/{PlayerId}");
        Roles("Admin");
        Summary(x => x.Summary = "Delete a pitcher");
    }

    public override async Task HandleAsync(DeletePitcherCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
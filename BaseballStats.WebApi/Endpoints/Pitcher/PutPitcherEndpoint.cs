using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Pitcher.Put;

namespace BaseballStats.WebApi.Endpoints.Pitcher;

public class PutPitcherEndpoint : Endpoint<PutPitcherCommand, PitcherDto>
{
    public override void Configure()
    {
        Put("pitchers");
        Roles("Admin");
        Summary(x => x.Summary = "Update a pitcher");
    }

    public override async Task HandleAsync(PutPitcherCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status201Created, ct);
    }
}


using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Game.UpdateAlignment;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Game;

public class UpdateAlignemntEndpoint : Endpoint<UpdateAlignmentCommand, SingleAlignmentDto>
{
    public override void Configure()
    {
        Put("/games/{GameId}/alignments/{TeamId}");
        AllowAnonymous();
        Summary(x => x.Summary = "Update a game alignment");
    }

    public override async Task HandleAsync(UpdateAlignmentCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
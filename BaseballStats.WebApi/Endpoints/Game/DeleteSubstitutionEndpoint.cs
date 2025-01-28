using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Game.DeleteSubstitution;

namespace BaseballStats.WebApi.Endpoints.Game;

public class DeleteSubstitutionEndpoint : Endpoint<DeleteSubstitutionCommand, SingleSubstitutionCRUDDto>
{
    public override void Configure()
    {
        Delete("/substitutions/{GameId}/{TeamId}/{PlayerInId}/{PlayerOutId}/{Time}");
        AllowAnonymous();
        Summary(x => x.Summary = "Delete a new substitution in the game");
    }

    public override async Task HandleAsync(DeleteSubstitutionCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
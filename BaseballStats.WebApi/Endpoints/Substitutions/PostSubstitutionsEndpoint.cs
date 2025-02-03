using FastEndpoints;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Substitutions.Post;

namespace BaseballStats.WebApi.Endpoints.Substitutions;

public class PostSubstitutionEndpoint : Endpoint<PostSubstitutionCommand, SingleSubstitutionCRUDDto>
{
    public override void Configure()
    {
        Post("/substitutions");
        Roles("Admin");
        Summary(x => x.Summary = "Add a new substitution to a game");
    }

    public override async Task HandleAsync(PostSubstitutionCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.User.Delete;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.User;

public class DeleteUserEndpoint : Endpoint<DeleteUserCommand, RegisteredUserDto>
{
    public override void Configure()
    {
        Delete("users/{Id}");
        Roles("Admin");
        // AllowAnonymous();
        Summary(x => x.Summary = "Elimina un usuario");
    }

    public override async Task HandleAsync(DeleteUserCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
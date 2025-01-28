using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.User.Put;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.User;

public class UpdateUserEndpoint : Endpoint<UpdateUserCommand, RegisteredUserDto>
{
    public override void Configure()
    {
        Put("users/{Id}");
        Roles("Admin");
        Summary(x => x.Summary = "Actualiza un usuario");
    }

    public override async Task HandleAsync(UpdateUserCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }

}
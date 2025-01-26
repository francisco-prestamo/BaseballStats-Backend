using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Auth;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.User;

public class PostUserEndpoint : Endpoint<RegisterUserCommand, RegisteredUserDto>
{
    public override void Configure()
    {
        Post("/users");
        Roles("Admin");
        // AllowAnonymous();
        Summary(x => x.Summary = "Crea un nuevo usuario");
    }

    public override async Task HandleAsync(RegisterUserCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status201Created, ct);
    }

}


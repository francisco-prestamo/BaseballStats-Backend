using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Auth;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.User;

public class CreateUserEndpoint : Endpoint<RegisterUserCommand, RegisteredUserDto>
{
    public override void Configure()
    {
        Post("/users");
        Roles("Admin");
        Summary(x => x.Summary = "Create a new user");
    }

    public override async Task HandleAsync(RegisterUserCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status201Created, ct);
    }

}


using BaseballStats.Application.Features.Auth;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Auth;

public class RegisterUsersEndpoint : Endpoint<RegisterUserCommand, RegisterUserResponse>
{
    public override void Configure()
    {
        Post("auth/register");
        Roles("Admin");
        Summary(x => x.Summary = "Register a new user");
    }

    public override async Task HandleAsync(RegisterUserCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, 200, ct);
    }
}
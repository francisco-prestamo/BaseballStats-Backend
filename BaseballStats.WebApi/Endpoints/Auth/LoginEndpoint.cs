using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Auth;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Auth;

public class LoginEndpoint : Endpoint<LoginCommand, RegisteredUserDto>
{
    public override void Configure()
    {
        Get("auth/login");
        AllowAnonymous();
        Summary(x => x.Summary = "Login a user");
    }

    public override async Task HandleAsync(LoginCommand command, CancellationToken ct)
    {
        var response = await command.ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
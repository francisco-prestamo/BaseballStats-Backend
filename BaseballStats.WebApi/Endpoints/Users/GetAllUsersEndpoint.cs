using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.User.GetAllUsers;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.User;

public class GetAllUsersEndpoint : EndpointWithoutRequest<List<RegisteredUserDto>>
{
    public override void Configure()
    {
        Get("/users");
        Roles("Admin");
        // AllowAnonymous();
        Summary(x => x.Summary = "Obtiene todos los usuarios");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetAllUsersCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
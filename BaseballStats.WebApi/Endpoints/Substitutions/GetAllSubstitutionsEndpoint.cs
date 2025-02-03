using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Substitutions.GetAll;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Substitutions;

public class GetAllSubstitutionsEndpoint : EndpointWithoutRequest<List<SingleSubstitutionCRUDDto>>
{
    public override void Configure()
    {
        Get("/substitutions");
        Roles("Admin", "Journalist", "TechnicalDirector");
        Summary(x => x.Summary = "Get all substitutions data");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetAllSubstitutionsCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }

}
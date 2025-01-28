using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.PlayerInPosition.GetAll;
using BaseballStats.Application.Features.PlayerInSeries.GetAll;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.PlayerInSeries;

public class GetAllPlayerInSeriesEndpoint : EndpointWithoutRequest<List<PlayerInSeriesCRUDDto>>
{
    public override void Configure()
    {
        Get("/playerInSeries");
        Roles("Journalist", "TechnicalDirector", "Admin");
        Summary(x => x.Summary = "Gets all team assignments for each player in each series");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await new GetAllPlayerInSeriesCommand().ExecuteAsync(ct);
        await SendAsync(response, StatusCodes.Status200OK, ct);
    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Season.Put;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Season;

public class PutSeasonEndpoint : Endpoint<PutSeasonCommand, SeasonDto>
{
    public override void Configure()
    {
        Put("seasons/{SeasonId}");
        Roles("Admin");
        Summary(x => x.Summary = "Update a season.");
    }

    public override async Task HandleAsync(PutSeasonCommand command, CancellationToken ct)
    {
        // var response = await command.ExecuteAsync(ct);
        var response = new SeasonDto();
        await SendAsync(response, StatusCodes.Status403Forbidden, ct);
    }
}
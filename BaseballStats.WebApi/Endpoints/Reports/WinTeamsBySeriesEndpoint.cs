using BaseballStats.Application.Features.Reports.WinTeamsBySeries;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Reports;

public class WinTeamsBySeriesEndpoint : Endpoint<WinTeamsBySeriesCommand, EmptyResponse>
{
    public override void Configure()
    {
        Get("reports/win-teams-by-series/{SeasonId}");
        Summary(x => x.Summary = "Generate a report of the winning teams by series for a given season.");
    }

    public override async Task HandleAsync(WinTeamsBySeriesCommand command, CancellationToken ct)
    {
        var file = await command.ExecuteAsync(ct);
        await SendFileAsync(file, "application/pdf", cancellation: ct);
    }
}
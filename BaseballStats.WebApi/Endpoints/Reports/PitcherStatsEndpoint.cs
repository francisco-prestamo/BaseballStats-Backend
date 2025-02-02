using FastEndpoints;
using BaseballStats.Application.Features.Reports.PitcherStats;

namespace BaseballStats.WebApi.Endpoints.Reports;

public class PitcherStatsEndpoint : EndpointWithoutRequest<FileInfo>
{
    public override void Configure()
    {
        Get("reports/pitchers-stats");
        Roles("Admin", "Journalist", "TechnicalDirector");
        Summary(x => x.Summary = "Gets Pitcher Statistics");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var file = await new PitcherStatsCommand().ExecuteAsync(ct);
        await SendFileAsync(file, "application/pdf", cancellation: ct);
    }

}


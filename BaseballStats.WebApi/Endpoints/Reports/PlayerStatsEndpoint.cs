using BaseballStats.Application.Features.Reports.PlayerStats;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Reports;

public class PlayerStatsEndpoint : Endpoint<PlayerStatsCommand, FileInfo>
{
    public override void Configure()
    {
        Get("reports/player-stats/{PlayerId}");
        Roles("Admin", "TechnicalDirector", "Journalist");
        Summary(x => x.Summary = "Gets a report on given player stats");
    }

    public override async Task HandleAsync(PlayerStatsCommand command, CancellationToken ct)
    {
        var file = await command.ExecuteAsync(ct);
        await SendFileAsync(file, "application/pdf", cancellation: ct);
    }
}
using FastEndpoints;
using BaseballStats.Application.Features.Reports.WinningAndLosingTeamsBySeries;

namespace BaseballStats.WebApi.Endpoints.Reports;

public class WinningAndLosingTeamsBySeriesEndpoint : EndpointWithoutRequest<EmptyResponse>
{
    public override void Configure()
    {
        Get("reports/winning-and-losing-teams-by-series"); 
        Roles("Admin", "TechnicalDirector", "Journalist");
        Summary(x => x.Summary = "Gets the winning and losing teams by series");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var file = await new WinningAndLosingTeamsBySeriesCommand().ExecuteAsync(cancellationToken);
        await SendFileAsync(file, "application/pdf", cancellation: cancellationToken);
    }
}

using FastEndpoints;
using BaseballStats.Application.Features.Reports.SeriesWithMostAndLeastGames;

namespace BaseballStats.WebApi.Endpoints.Reports;

public class SeriesWithMostAndLeastGamesEndpoint : EndpointWithoutRequest<EmptyResponse>
{
    public override void Configure()
    {
        Get("reports/series/with-most-and-least-games");
        Roles("Admin", "TechnicalDirector", "Journalist");
        Summary(x => x.Summary = "Gets the series with the most and least games for each season");
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)

    {
        var file = await new SeriesWithMostAndLeastGamesCommand().ExecuteAsync(cancellationToken);
        await SendFileAsync(file, "application/pdf", cancellation: cancellationToken);
    }

}



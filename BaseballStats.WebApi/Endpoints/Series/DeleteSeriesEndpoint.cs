using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Series.Delete;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Series;

public class DeleteSeriesEndpoint : Endpoint<DeleteSeriesCommand, SeriesDto>
{
    public override void Configure()
    {
        Delete("series/{SeasonId}/{Id}");
        Roles("Admin");
        Summary(x => x.Summary = "Delete a series");
    }

    public override async Task HandleAsync(DeleteSeriesCommand command, CancellationToken cancellationToken)
    {
        var response = await command.ExecuteAsync(cancellationToken);
        await SendAsync(response, 200, cancellationToken);
    }
}
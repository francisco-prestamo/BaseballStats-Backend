using BaseballStats.Application.DTOs;
using BaseballStats.Application.Features.Season.Delete;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Season;

public class DeleteEndpoint : Endpoint<DeleteSeasonCommand, SeasonDto>
{
    public override void Configure()
    {
        Delete("seasons/{Id}");
        Roles("Admin");
        Summary(x => x.Summary = "Delete a season");
    }

    public override async Task HandleAsync(DeleteSeasonCommand command, CancellationToken cancellationToken)
    {
        var response = await command.ExecuteAsync(cancellationToken);
        await SendAsync(response, StatusCodes.Status200OK, cancellationToken);
    }
}
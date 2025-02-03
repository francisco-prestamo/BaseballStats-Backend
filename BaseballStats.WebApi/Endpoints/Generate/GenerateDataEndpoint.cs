using BaseballStats.Application.Features.GenerateData;
using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.Generate;

public class GenerateDataEndpoint : EndpointWithoutRequest<EmptyResponse>
{
    public override void Configure()
    {
        Get("generate-data");
        AllowAnonymous();
        
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var command = new GenerateDataCommand();
        await command.ExecuteAsync(ct);

        await SendAsync(new EmptyResponse(), StatusCodes.Status200OK, ct);
    }
}

using FastEndpoints;

namespace BaseballStats.WebApi.Endpoints.ExampleEndpoint;

public class ExampleEndpoint : Endpoint<EmptyRequest, EmptyResponse>
{
    public override void Configure()
    {
        Get("api/example");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(EmptyRequest request, CancellationToken cancellationToken)
    {
        await SendAsync(new EmptyResponse(), cancellation: cancellationToken);
    }
}
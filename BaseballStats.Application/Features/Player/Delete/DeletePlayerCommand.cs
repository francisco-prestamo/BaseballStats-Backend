using FastEndpoints;

namespace BaseballStats.Application.Features.Player.Delete;

// ReSharper disable once ClassNeverInstantiated.Global
public record DeletePlayerCommand : ICommand<EmptyResponse>
{
    public long PlayerId { get; init; }
}
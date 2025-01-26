using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInPosition.Post;

public record PostPlayerInPositionCommand : ICommand<PlayerInPositionCRUDDto>
{
    public long PlayerId { get; init; }
    public required string Position { get; init; } = null!;
    public required double Effectiveness { get; init; }
}
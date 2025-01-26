using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInPosition.Get;

public record GetPlayerInPositionCommand : ICommand<PlayerInPositionCRUDDto>
{
    public long PlayerId { get; init; }
    public string Position { get; init; } = null!;
}
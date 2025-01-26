using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInPosition.Delete;

public record DeletePlayerInPositionCommand : ICommand<PlayerInPositionCRUDDto>
{
    public long PlayerId { get; init; }
    public string Position { get; init; } = null!;
}
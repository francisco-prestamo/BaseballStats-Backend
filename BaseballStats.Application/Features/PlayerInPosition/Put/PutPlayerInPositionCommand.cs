using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInPosition.Put;

public record PutPlayerInPositionCommand : ICommand<PlayerInPositionCRUDDto>
{
    public long PlayerId { get; init; }
    public string Position { get; init; } = null!;

    public required double Effectiveness { get; init; }
}
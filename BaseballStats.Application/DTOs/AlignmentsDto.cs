using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.DTOs;

public class PlayerInPosition
{
    public long PlayerId { get; set; }
    public string Position { get; set; } = null!;
    public PlayerInPosition(long playerId, string position)
    {
        PlayerId = playerId;
        Position = position;
    }
}

public record AlignmentsDto
{
    public long Team1Id { get; init; }
    public List<PlayerInPosition> Team1Alignment { get; init; } = null!;
    public long Team2Id { get; init; }
    public List<PlayerInPosition> Team2Alignment { get; init; } = null!;
}

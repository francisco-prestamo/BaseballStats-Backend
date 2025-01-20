using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.DTOs;

public record AlignmentsDto
{
    public long Team1Id { get; init; }
    public List<PlayerInPositionDto> Team1Alignment { get; init; } = null!;
    public long Team2Id { get; init; }
    public List<PlayerInPositionDto> Team2Alignment { get; init; } = null!;
}

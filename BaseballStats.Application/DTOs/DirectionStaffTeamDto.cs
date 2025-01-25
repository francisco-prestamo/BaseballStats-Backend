namespace BaseballStats.Application.DTOs;

public record DirectionStaffTeamDto
{
    public long DirectionMemberId { get; init; }
    public long TeamId { get; init; }
}
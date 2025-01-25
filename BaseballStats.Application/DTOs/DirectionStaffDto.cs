namespace BaseballStats.Application.DTOs;

public record DirectionStaffDto
{
    public long Id { get; init; }
    public string Name { get; init; } = null!;
}
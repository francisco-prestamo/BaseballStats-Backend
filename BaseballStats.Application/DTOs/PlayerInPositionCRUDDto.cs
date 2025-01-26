namespace BaseballStats.Application.DTOs;

public record PlayerInPositionCRUDDto
{
    public long PlayerId { get; init; }
    public string Position { get; init; } = null!;
    public double Effectiveness { get; init; }
}
namespace BaseballStats.Application.DTOs;

public record GameDto
{
    public long Id { get; init; }
    public TeamDto Team1 { get; init; } = null!;
    public TeamDto Team2 { get; init; } = null!;
    public DateOnly Date { get; init; }
    public bool WinTeam { get; init; }
    public int Team1Runs { get; init; }
    public int Team2Runs { get; init; }
}
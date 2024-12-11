namespace BaseballStats.Application.DTOs;

public record GameDto
{
    public long id { get; init; }
    public TeamDto team1 { get; init; } = null!;
    public TeamDto team2 { get; init; } = null!;
    public DateOnly date { get; init; }
    public bool winTeam { get; init; }
    public int team1Runs { get; init; }
    public int team2Runs { get; init; }
}
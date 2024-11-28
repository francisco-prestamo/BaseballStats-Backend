namespace BaseballStats.Application.DTOs;

public record GameDto
{
    public long Id { get; init; }
    public long Team1Id { get; init; }
    public long Team2Id { get; init; }
    public DateOnly Date { get; init; }
    public bool Winner1 { get; init; }
    public int Runs1 { get; init; }
    public int Runs2 { get; init; }
    public long SeriesId { get; init; }
}
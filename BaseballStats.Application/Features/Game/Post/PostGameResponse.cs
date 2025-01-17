namespace BaseballStats.Application.Features.Game.Post;

public record PostGameResponse
{
    public long Id { get; init; }
    public long Team1Id { get; init; }
    public long Team2Id { get; init; }
    public DateOnly Date { get; init; }
    public bool WinTeam { get; init; }
    public int Team1Runs { get; init; }
    public int Team2Runs { get; init; }
    public long SeriesId { get; init; }
}
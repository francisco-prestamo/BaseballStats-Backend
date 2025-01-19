namespace BaseballStats.Domain.ResultSets;

public class TeamWithExtras
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string RepresentedEntity { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public long TechnicalDirectorId { get; set; }
    public int WinGames { get; set; }
    public int LoseGames { get; set; }
    public int TotalRuns { get; set; }
}
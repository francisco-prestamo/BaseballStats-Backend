using BaseballStats.Domain.Entities;

public class TeamWithExtras
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string RepresentedEntity { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public long TechnicalDirectorId { get; set; }
    public int winGames {get; set;}
    public int loseGames {get; set;}
    public int totalRuns {get; set;}
}
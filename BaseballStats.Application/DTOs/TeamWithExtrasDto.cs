namespace BaseballStats.Application.DTOs;

public record TeamWithExtrasDto : TeamDto 
{
    public long winGames { get; set; }
    public long loseGames { get; set; }
    public long totalRuns { get; set; }
}
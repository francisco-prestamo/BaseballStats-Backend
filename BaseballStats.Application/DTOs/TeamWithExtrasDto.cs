namespace BaseballStats.Application.DTOs;

public record TeamWithExtrasDto : TeamDto 
{
    public long WinGames { get; set; }
    public long LoseGames { get; set; }
    public long TotalRuns { get; set; }
}
namespace BaseballStats.Application.Features.Reports.WinTeamsBySeries;

public record SeriesStatsDto
{
    public long SerieId { get; init; }
    public string SerieName { get; init; } = string.Empty;
    public long TeamId { get; init; }
    public string TeamWinnerName { get; init; } = string.Empty;
    public long WinGames { get; init; }
    public string TechnicalDirector { get; init; } = string.Empty;
}
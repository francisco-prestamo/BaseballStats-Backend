namespace BaseballStats.Application.Features.Reports.WinTeamsBySeries;

public record WinTeamsBySeriesDto
{
    public long SeasonId { get; init; }
    public List<SeriesStatsDto> SeriesStatsDto { get; init; } = [];
}
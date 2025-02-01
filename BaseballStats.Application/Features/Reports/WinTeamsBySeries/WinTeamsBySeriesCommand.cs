using FastEndpoints;

namespace BaseballStats.Application.Features.Reports.WinTeamsBySeries;

public record WinTeamsBySeriesCommand : ICommand<FileInfo>
{
    public long SeasonId { get; init; }
}
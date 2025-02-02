using FastEndpoints;

namespace BaseballStats.Application.Features.Reports.PlayerStats;

public class PlayerStatsCommand : ICommand<FileInfo>
{
    public long PlayerId { get; init; }
}



using FastEndpoints;

namespace BaseballStats.Application.Features.Reports.TeamStarPlayers;

public record TeamStarPlayersCommand : ICommand<FileInfo>
{
    public long SeasonId { get; init; }
    public long SeriesId { get; init; }
    public long TeamId { get; init; }
}
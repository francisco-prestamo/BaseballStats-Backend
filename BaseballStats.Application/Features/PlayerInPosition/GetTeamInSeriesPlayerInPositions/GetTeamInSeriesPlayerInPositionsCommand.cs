using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInPosition.GetTeamInSeriesPlayerInPositions;

public record GetTeamInSeriesPlayerInPositionsCommand : ICommand<List<PlayerInPositionCRUDDto>>
{
    public long SeriesId { get; init; }
    public long SeasonId { get; init; }
    public long TeamId { get; init; }
}

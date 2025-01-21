using FastEndpoints;
using BaseballStats.Domain.Enums;
using BaseballStats.Application.DTOs;

namespace BaseballStats.Application.Features.Player.GetPlayerPositionsInSeries;

public record GetPlayerPositionsInSeriesCommand : ICommand<List<PlayerInPositionDto>>
{
    public long PlayerId { get; init; }
    public long SeasonId { get; init; }
    public long SeriesId { get; init; }
}
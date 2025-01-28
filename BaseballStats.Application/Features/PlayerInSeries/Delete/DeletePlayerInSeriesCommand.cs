using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInSeries.Delete;

public record DeletePlayerInSeriesCommand : ICommand<PlayerInSeriesCRUDDto>
{
    public long PlayerId { get; init; }
    public long SerieId { get; init; }
    public long SeasonId { get; init; }
}
using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInSeries.Put;

public record PutPlayerInSeriesCommand : ICommand<PlayerInSeriesCRUDDto>
{
    public long PlayerId { get; init; }
    public long SerieId { get; init; }
    public long SeasonId { get; init; }
    public long TeamId { get; init; }
}
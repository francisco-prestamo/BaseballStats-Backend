using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInSeries.Get
{
    public record GetPlayerInSeriesCommand : ICommand<PlayerInSeriesCRUDDto>
    {
        public long PlayerId { get; init; }
        public long SerieId { get; init; }
        public long SeasonId { get; init; }
    }
}
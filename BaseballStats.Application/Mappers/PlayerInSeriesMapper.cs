using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class PlayerInSeriesMapper
{
    public static PlayerInSeriesCRUDDto ToCRUDDto(this PlayerInSeries entity, long seasonId)
    {
        return new PlayerInSeriesCRUDDto
        {
            PlayerId = entity.PlayerId,
            SerieId = entity.SeriesId,
            SeasonId = seasonId,
            TeamId = entity.TeamId ?? 0
        };
    }
}
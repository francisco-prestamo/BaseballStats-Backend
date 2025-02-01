using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class StarPlayerInPositionMapper
{
    public static StarPlayerInPositionDto ToDto(this StarPlayerInPosition starPlayerInPosition, long seasonId)
    {
        return new StarPlayerInPositionDto()
        {
            PlayerId = starPlayerInPosition.PlayerId,
            Position = starPlayerInPosition.Position.GetDisplayName(),
            SeriesId = starPlayerInPosition.SeriesId,
            SeasonId = seasonId
        };
    }
}

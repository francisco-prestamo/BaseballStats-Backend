using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class SeriesMapper
{
    public static SeriesDto ToDto(this Series series)
    {
        return new SeriesDto()
        {
            id = series.Id,
            name = series.Name,
            type = series.Type,
            startDate = series.StartDate,
            sndDate = series.EndDate,
            idSeason = series.SeasonId
        };
    }
}
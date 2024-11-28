using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class SeriesMapper
{
    public static SeriesDto ToDto(this Series series)
    {
        return new SeriesDto()
        {
            Id = series.Id,
            Name = series.Name,
            Type = series.Type,
            StartDate = series.StartDate,
            EndDate = series.EndDate,
            SeasonId = series.SeasonId
        };
    }
}
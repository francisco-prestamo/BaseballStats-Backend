using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class SeasonMapper
{
    public static SeasonDto ToDto(this Season season)
    {
        return new SeasonDto()
        {
            Id = season.Id
        };
    }
}
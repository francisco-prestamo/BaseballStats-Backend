using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Application.ResultSets;

namespace BaseballStats.Application.Mappers;

public static class RegularPlayerMapper
{
    public static RegularPlayerDto ToDto(this Player player)
    {
        return new RegularPlayerDto()
        {
            Id = player.Id,
            Name = player.Name,
            Age = player.Age,
            YearsOfExperience = player.YearsOfExperience,
            BattingAverage = player.BattingAverage
        };
    }

    public static RegularPlayerDto GetRegularPlayerDto(this Alignment alignment)
    {
        return new RegularPlayerDto()
        {
            Id = alignment.Id,
            Name = alignment.Name,
            Age = alignment.Age,
            YearsOfExperience = alignment.YearsOfExperience,
            BattingAverage = alignment.BattingAverage,
        };
    }

}
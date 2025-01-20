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
            Id = alignment.PlayerId,
            Name = alignment.PlayerName,
            Age = alignment.PlayerAge,
            YearsOfExperience = alignment.PlayerYearsOfExperience,
            BattingAverage = alignment.PlayerBattingAverage
        };
    }

}
using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class PlayerMapper
{
    public static PlayerDto ToDto(this Player player)
    {
        return new PlayerDto()
        {
            Id = player.Id,
            Name = player.Name,
            Age = player.Age,
            YearsOfExperience = player.YearsOfExperience,
            BattingAverage = player.BattingAverage
        };
    }
}
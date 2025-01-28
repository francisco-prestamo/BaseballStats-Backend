using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Application.ResultSets;

namespace BaseballStats.Application.Mappers;

public static class PitcherMapper
{
    public static PitcherDto ToDto(this Pitcher pitcher)
    {
        return new PitcherDto()
        {
            Id = pitcher.Id,
            Name = pitcher.Player.Name,
            Age = pitcher.Player.Age,
            YearsOfExperience = pitcher.Player.YearsOfExperience,
            BattingAverage = pitcher.Player.BattingAverage,
            GamesWonNumber = pitcher.GamesWonNumber,
            GamesLostNumber = pitcher.GamesLostNumber,
            RightHanded = pitcher.RightHanded,
            AllowedRunsAvg = pitcher.AllowedRunsAvg
        };
    }

}
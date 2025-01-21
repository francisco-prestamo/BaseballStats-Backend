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
            Name = pitcher.Name,
            Age = pitcher.Age,
            YearsOfExperience = pitcher.YearsOfExperience,
            BattingAverage = pitcher.BattingAverage,
            GamesWonNumber = pitcher.GamesWonNumber,
            GamesLostNumber = pitcher.GamesLostNumber,
            RightHanded = pitcher.RightHanded,
            AllowedRunsAvg = pitcher.AllowedRunsAvg
        };
    }

}
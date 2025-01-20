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

    public static PitcherDto GetPitcherDto(this Alignment alignment)
    {
        if (alignment.GamesLostNumber == null ||
            alignment.GamesWonNumber == null || 
            alignment.AllowedRunsAvg == null ||
            alignment.RightHanded == null)
        {
            throw new Exception("The player is not a pitcher");
        }

        return new PitcherDto()
        {
            Id = alignment.PlayerId,
            Name = alignment.PlayerName,
            Age = alignment.PlayerAge,
            YearsOfExperience = alignment.PlayerYearsOfExperience,
            BattingAverage = alignment.PlayerBattingAverage,
            GamesWonNumber = (int)alignment.GamesWonNumber,
            GamesLostNumber = (int)alignment.GamesLostNumber,
            RightHanded = (bool)alignment.RightHanded,
            AllowedRunsAvg = (double)alignment.AllowedRunsAvg
        };
    }
}
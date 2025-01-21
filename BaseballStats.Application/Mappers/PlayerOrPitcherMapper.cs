using BaseballStats.Application.DTOs;
using BaseballStats.Application.ResultSets;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class PlayerOrPitcherMapper
{
    public static PlayerOrPitcherDto ToDto(this PlayerPitcher playerPitcher)
    {
        if (playerPitcher.GamesLostNumber != null)
        {
            return new PlayerOrPitcherDto()
            {
                Pitcher = new PitcherDto()
                {
                    Id = playerPitcher.PlayerId,
                    Name = playerPitcher.PlayerName,
                    Age = playerPitcher.PlayerAge,
                    YearsOfExperience = playerPitcher.PlayerYearsOfExperience,
                    BattingAverage = playerPitcher.PlayerBattingAverage,
                    GamesWonNumber = (int)playerPitcher.GamesWonNumber!,
                    GamesLostNumber = (int)playerPitcher.GamesLostNumber!,
                    RightHanded = (bool)playerPitcher.RightHanded!,
                    AllowedRunsAvg = (double)playerPitcher.AllowedRunsAvg!   
                },
                Player = null
            };
        }
        return new PlayerOrPitcherDto()
        {
            Pitcher = null,
            Player = new RegularPlayerDto()
            {
                Id = playerPitcher.PlayerId,
                Name = playerPitcher.PlayerName,
                Age = playerPitcher.PlayerAge,
                YearsOfExperience = playerPitcher.PlayerYearsOfExperience,
                BattingAverage = playerPitcher.PlayerBattingAverage
            }
        };
    }
}
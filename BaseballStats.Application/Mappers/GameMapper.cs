using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class GameMapper
{
    public static GameDto ToDto(this Game game)
    {
        return new GameDto()
        {
            Team1Id = game.Team1Id,
            Team2Id = game.Team2Id,
            Date = game.Date,
            Winner1 = game.Winner1,
            Runs1 = game.Runs1,
            Runs2 = game.Runs2,
            SeriesId = game.SeriesId
        };
    }
}
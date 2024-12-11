using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class GameMapper
{
    public static GameDto ToDto(this Game game, TeamDto team1, TeamDto team2)
    {
        return new GameDto()
        {
            id = game.Id,
            team1 = team1,
            team2 = team2,
            date = game.Date,
            winTeam = game.Winner1,
            team1Runs = game.Runs1,
            team2Runs = game.Runs2,
        };
    }
}
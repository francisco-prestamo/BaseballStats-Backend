using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class GameMapper
{
    public static GameDto ToDto(this Game game, TeamDto team1, TeamDto team2)
    {
        return new GameDto()
        {
            Id = game.Id,
            Team1 = team1,
            Team2 = team2,
            Date = game.Date,
            WinTeam = game.Winner1,
            Team1Runs = game.Runs1,
            Team2Runs = game.Runs2,
        };
    }
}
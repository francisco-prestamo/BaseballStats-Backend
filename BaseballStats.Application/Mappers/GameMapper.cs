using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Mappers;

public static class GameMapper
{
    public static GameWithTeamsDto ToDto(this Game game, TeamDto team1, TeamDto team2)
    {
        return new GameWithTeamsDto()
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
    
    public static GameDto ToGameDto(this Game game)
    {
        return new GameDto()
        {
            Id = game.Id,
            Team1Id = game.Team1Id,
            Team2Id = game.Team2Id,
            Date = game.Date,
            WinTeam = game.Winner1,
            Team1Runs = game.Runs1,
            Team2Runs = game.Runs2,
            SeriesId = game.SeriesId
        };
    }
}
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Application.ResultSets;
using BaseballStats.Domain.Enums;
using BaseballStats.Application.Mappers;
using Microsoft.EntityFrameworkCore;

namespace BaseballStats.Application.Services;

public class TeamWithExtrasService(IUnitOfWork unitOfWork)
{
    public Task<IEnumerable<TeamWithExtras>> GetTeamsWithExtrasAsync()
    {
        var teams = unitOfWork.Repository<Team>().DbSet;
        var games = unitOfWork.Repository<Game>().DbSet;

        var teamsWithExtras = from team in teams
                              join game in games on team.Id equals game.Team1Id into team1Games
                              from game1 in team1Games.DefaultIfEmpty()
                              join game in games on team.Id equals game.Team2Id into team2Games
                              from game2 in team2Games.DefaultIfEmpty()
                              group new { team, game1, game2 } by team.Id
            into g
                              select new TeamWithExtras
                              {
                                  Id = g.Key,
                                  Name = g.First().team.Name,
                                  Initials = g.First().team.Initials,
                                  Color = g.First().team.Color,
                                  RepresentedEntity = g.First().team.RepresentedEntity,
                                  TotalRuns = g.Sum(tg => tg.team.Id == tg.game1.Team1Id ? tg.game1.Runs1 : tg.game2.Runs2),
                                  WinGames = g.Sum(tg => tg.team.Id == tg.game1.Team1Id ? (tg.game1.Winner1 ? 1 : 0) : (tg.game2.Winner1 ? 0 : 1)),
                                  LoseGames = g.Sum(tg => tg.team.Id == tg.game1.Team1Id ? (tg.game1.Winner1 ? 0 : 1) : (tg.game2.Winner1 ? 1 : 0))
                              };

        return Task.FromResult(teamsWithExtras.AsEnumerable());
    }

    public Task<IEnumerable<TeamWithExtras>> GetTeamsWithExtrasThatPlayedInSeriesAsync(long seriesId)
    {
        var teams = unitOfWork.Repository<Team>().DbSet;
        var games = unitOfWork.Repository<Game>().DbSet;

        var relevantTeamIds = (
            from game in games
            where game.SeriesId == seriesId
            group game by game.Team1Id into team1Data
            select new {TeamId = team1Data.Key}
        ).Union(
            from game in games
            where game.SeriesId == seriesId
            group game by game.Team2Id into team2Data
            select new {TeamId = team2Data.Key}
        );

        var relevantTeamData = (
            from teamId in relevantTeamIds
            join team in teams on teamId.TeamId equals team.Id
            select team
        ).AsNoTracking().ToList();

        var team1sData = (
            from g in games
            where g.SeriesId == seriesId
            group g by g.Team1Id into t1data
            select new
            {
                TeamId = t1data.Key,
                TotalRuns = t1data.Sum(x => x.Runs1),
                WinGames = t1data.Count(x => x.Winner1),
                LoseGames = t1data.Count(x => !x.Winner1)
            }
        ).ToList();

        var team2sData = (
            from g in games
            where g.SeriesId == seriesId
            group g by g.Team2Id into t2data
            select new
            {
                TeamId = t2data.Key,
                TotalRuns = t2data.Sum(x => x.Runs2),
                WinGames = t2data.Count(x => !x.Winner1),
                LoseGames = t2data.Count(x => x.Winner1)
            }
        ).ToList();

        var answer = (
            from teamData in team1sData.Concat(team2sData)
            group teamData by teamData.TeamId into teamDataGroup
            join team in relevantTeamData on teamDataGroup.Key equals team.Id
            select new TeamWithExtras
            {
                Id = teamDataGroup.Key,
                Name = team.Name,
                Initials = team.Initials,
                Color = team.Color,
                RepresentedEntity = team.RepresentedEntity,
                TechnicalDirectorId = team.TechnicalDirectorId,
                TotalRuns = teamDataGroup.Sum(x => x.TotalRuns),
                WinGames = teamDataGroup.Sum(x => x.WinGames),
                LoseGames = teamDataGroup.Sum(x => x.LoseGames)
            }
        );

        var teamWithExtrasEnumerable = answer.AsEnumerable();
        

        return Task.FromResult(teamWithExtrasEnumerable);
    }
}
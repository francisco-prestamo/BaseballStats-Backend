using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Application.ResultSets;
using BaseballStats.Domain.Enums;
using BaseballStats.Application.Mappers;

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

    public Task<IEnumerable<TeamWithExtras>> GetTeamsWithExtrasWithPlayerInSeriesAsync(long seriesId)
    {
        var teams = unitOfWork.Repository<Team>().DbSet;
        var games = unitOfWork.Repository<Game>().DbSet;
        var playersInSeries = unitOfWork.Repository<PlayerInSeries>().DbSet;

        var teamsWithExtras = from twe in (
                from team in teams
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
                }
            )
                              join pis in playersInSeries on twe.Id equals pis.TeamId
                              where pis.SeriesId == seriesId
                              select twe;

        var teamWithExtrasEnumerable = teamsWithExtras.AsEnumerable().DistinctBy(x => x.Id);

        return Task.FromResult(teamWithExtrasEnumerable);
    }
}
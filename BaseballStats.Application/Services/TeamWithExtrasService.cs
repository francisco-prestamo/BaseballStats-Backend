using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Application.ResultSets;
using BaseballStats.Domain.Enums;
using BaseballStats.Application.Mappers;

namespace BaseballStats.Application.Services;

public class TeamWithExtrasService(AlignmentService alignmentsService, IUnitOfWork unitOfWork)
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

    public Task<IEnumerable<DTOs.Substitution>> GetSubstitutionsWithExtrasAsync(long gameId, long teamId)
    {
        var game = unitOfWork.Repository<Game>().GetByIdAsync(gameId)!;
        var substitutionsRepository = unitOfWork.Repository<Substitution>();
        var playerInPosition_table = unitOfWork.Repository<PlayerInPosition>().DbSet;
        var player_table = unitOfWork.Repository<Player>().DbSet;

        var playerInPosition =
            (from p in playerInPosition_table
             select new { p.PlayerId, p.Position, p.Effectiveness }).ToDictionary(x => (x.PlayerId, x.Position));

        var alignment = alignmentsService.GetAlignmentsFromGame(gameId, teamId);

        Dictionary<long, (PlayerPositions, double)> positions = new();
        foreach (var player in alignment.Result)
            positions.Add(player.Id, (player.Position, player.Effectiveness));

        var substitutions = substitutionsRepository.Where(x => x.GameId == gameId && x.TeamId == teamId).ToList();
        substitutions.Sort((x, y) => x.Time.CompareTo(y.Time));

        List<DTOs.Substitution> ret = new();
        foreach (var substitution in substitutions)
        {
            var playerOut = new PlayerInPosition
            {
                PlayerId = substitution.PlayerOutId,
                Position = positions[substitution.PlayerOutId].Item1,
                Effectiveness = positions[substitution.PlayerOutId].Item2,
            };

            var oldPosition = positions[substitution.PlayerOutId].Item1;
            
            positions.Add(substitution.PlayerInId, (oldPosition, playerInPosition[(substitution.PlayerInId, oldPosition)].Effectiveness));

            var playerIn = new PlayerInPosition
            {
                PlayerId = substitution.PlayerInId,
                Position = positions[substitution.PlayerInId].Item1,
                Effectiveness = positions[substitution.PlayerInId].Item2,
            };

            ret.Add(new DTOs.Substitution(teamId, playerIn, playerOut, substitution.Time));
        }

        return Task.FromResult(ret.AsEnumerable());
    }
}
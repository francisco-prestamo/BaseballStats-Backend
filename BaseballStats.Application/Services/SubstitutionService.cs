using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Enums;
using BaseballStats.Application.ResultSets;
using FastEndpoints;

namespace BaseballStats.Application.Services;

public class SubstitutionService(IUnitOfWork unitOfWork)
{
    public async Task<IEnumerable<SubstitutionWithPosition>> GetSubstitutionsWithExtrasAsync(long gameId, long teamId)
    {
        var substitutions_table = unitOfWork.Repository<Substitution>().DbSet;
        var alignedPlayerInGame_table = unitOfWork.Repository<AlignedPlayerInGame>().DbSet;
        var alignment =
            from apig in alignedPlayerInGame_table
            where apig.GameId == gameId && apig.TeamId == teamId
            select new InitialAlignment
            {
                PlayerId = apig.PlayerId,
                Position = apig.Position
            };


        var substitutions = 
            from s in substitutions_table
            where s.GameId == gameId && s.TeamId == teamId
            select s;
        
        return await Task.FromResult(GetSubstitutionWithPositions(alignment, substitutions));
    }

    public async Task<List<(long gameId, List<SubstitutionWithPosition> substitutionWithPositions, List<InitialAlignment> initialAlignments)>> GetInitialAlignmemntsAndSubstitutionsForTeamInSeries(long teamId, long seriesId)
    {
        var alignedPlayerInGame_table = unitOfWork.Repository<AlignedPlayerInGame>().DbSet;
        var game_table = unitOfWork.Repository<Game>().DbSet;
        var playerInSeries_table = unitOfWork.Repository<PlayerInSeries>().DbSet;
        var substitution_table = unitOfWork.Repository<Substitution>().DbSet;


        var initialAlignmentsOfTeamGamesInTheSeries = (
            from apig in alignedPlayerInGame_table
            join game in game_table on apig.GameId equals game.Id
            where game.SeriesId == seriesId && apig.TeamId == teamId
            select apig
        ).ToList();

        System.Console.WriteLine(string.Join(", ", initialAlignmentsOfTeamGamesInTheSeries.Select(x => $"{x.GameId} - {x.PlayerId} - {x.Position}")));

        var gamesInSeries =
            from ia in initialAlignmentsOfTeamGamesInTheSeries
            group ia by ia.GameId into g
            select g.Key;

        var allTeamSubstitutionsInSeries = (
            from s in substitution_table
            join g in game_table on s.GameId equals g.Id
            where g.SeriesId == seriesId && s.TeamId == teamId
            select s
        ).ToList();

        var ret = new List<(long gameId, List<SubstitutionWithPosition>, List<InitialAlignment>)>();
        foreach (var gameId in gamesInSeries)
        {
            var initialAlignment =
                from ia in initialAlignmentsOfTeamGamesInTheSeries
                where ia.GameId == gameId
                select new InitialAlignment
                {
                    PlayerId = ia.PlayerId,
                    Position = ia.Position
                };

            var substitutions = 
                from s in allTeamSubstitutionsInSeries
                where s.GameId == gameId
                select s;

            var substitutionsWithPositions = GetSubstitutionWithPositions(initialAlignment.AsQueryable(), substitutions.AsQueryable());

            ret.Add((gameId, substitutionsWithPositions, initialAlignment.ToList()));
        }

        return await Task.FromResult(ret);
    }

    private List<SubstitutionWithPosition> GetSubstitutionWithPositions(IQueryable<InitialAlignment> initialAlignment, IQueryable<Substitution> substitutions)
    {
        Dictionary<long, PlayerPositions> positions = new();
        foreach (var player in initialAlignment)
            positions.Add(player.PlayerId, player.Position);

        var sortedSubstitutions = 
            from s in substitutions
            orderby s.Time ascending
            select s;

        List<SubstitutionWithPosition> ret = new();
        foreach (var substitution in sortedSubstitutions)
        {
            var playerOutId = substitution.PlayerOutId;
            var oldPosition = positions[substitution.PlayerOutId];
            
            positions.Add(substitution.PlayerInId, oldPosition);

            var playerInId = substitution.PlayerInId;

            ret.Add(new SubstitutionWithPosition(){
                    PlayerInId = playerInId,
                    PlayerOutId = playerOutId,
                    Position = oldPosition,
                    Time = substitution.Time
                }
            );
        }

        return ret;
    }

}

public class InitialAlignment
{
    public long PlayerId { get; set; }
    public PlayerPositions Position { get; set; }
}

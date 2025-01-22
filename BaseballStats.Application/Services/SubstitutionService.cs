using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Enums;
using BaseballStats.Application.ResultSets;
using FastEndpoints;

namespace BaseballStats.Application.Services;

public class SubstitutionService(IUnitOfWork unitOfWork, AlignmentService alignmentService)
{
    public async Task<IEnumerable<SubstitutionWithPosition>> GetSubstitutionsWithExtrasAsync(long gameId, long teamId)
    {
        var game = (await unitOfWork.Repository<Game>().GetByIdAsync(gameId))!;
        var substitutionsRepository = unitOfWork.Repository<Substitution>();
        var playerInPosition_table = unitOfWork.Repository<PlayerInPosition>().DbSet;
        var playerInSeries_table = unitOfWork.Repository<PlayerInSeries>().DbSet;

        var seriesId = game.SeriesId;

        var playersInTeam = 
            from pis in playerInSeries_table
            where pis.TeamId == teamId && pis.SeriesId == seriesId
            select pis.PlayerId;

        var playerInPosition = (
            from p in playerInPosition_table
            join pitId in playersInTeam on p.PlayerId equals pitId
            select new { p.PlayerId, p.Position, p.Effectiveness }
        ).ToDictionary(x => (x.PlayerId, x.Position));

        var alignment = alignmentService.GetAlignmentsFromGame(gameId, teamId);

        Dictionary<long, PlayerPositions> positions = new();
        foreach (var player in alignment.Result)
            positions.Add(player.Id, player.Position);

        var substitutions = substitutionsRepository.Where(x => x.GameId == gameId && x.TeamId == teamId).ToList();
        substitutions.Sort((x, y) => x.Time.CompareTo(y.Time));

        List<SubstitutionWithPosition> ret = new();
        foreach (var substitution in substitutions)
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


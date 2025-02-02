using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Enums;
using BaseballStats.Application.ResultSets;
using FastEndpoints;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace BaseballStats.Application.Services;

public class SubstitutionService(IUnitOfWork unitOfWork)
{
    public async Task<IEnumerable<SubstitutionWithPosition>> GetSubstitutionsWithExtrasAsync(long gameId, long teamId)
    {
        var substitutions_table = unitOfWork.Repository<Substitution>().DbSet;
        var alignedPlayerInGame_table = unitOfWork.Repository<AlignedPlayerInGame>().DbSet;

        var alignment = (
            from apig in alignedPlayerInGame_table
            where apig.GameId == gameId && apig.TeamId == teamId
            select new
            {
                apig.PlayerId,
                apig.Position
            }).ToList().Select(x => (x.PlayerId, x.Position));


        var substitutions =
            from s in substitutions_table
            where s.GameId == gameId && s.TeamId == teamId
            select s;
        
        return await Task.FromResult(GetSubstitutionWithPositionsForATeamInAGame(alignment, substitutions));
    }

    public async Task<List<(long gameId, List<SubstitutionWithPosition> substitutionWithPositions, List<(long PlayerId, PlayerPositions Position)> initialAlignments)>> GetInitialAlignmemntsAndSubstitutionsForTeamInSeries(long teamId, long seriesId)
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

        var ret = new List<(long gameId, List<SubstitutionWithPosition>, List<(long PlayerId, PlayerPositions Position)>)>();
        foreach (var gameId in gamesInSeries)
        {
            var initialAlignment = (
                from ia in initialAlignmentsOfTeamGamesInTheSeries
                where ia.GameId == gameId
                select new
                {
                    PlayerId = ia.PlayerId,
                    Position = ia.Position
                }).ToList().Select(x => (x.PlayerId, x.Position));

            var substitutions =
                from s in allTeamSubstitutionsInSeries
                where s.GameId == gameId
                select s;

            var substitutionsWithPositions = GetSubstitutionWithPositionsForATeamInAGame(initialAlignment, substitutions.AsQueryable());

            ret.Add((gameId, substitutionsWithPositions, initialAlignment.ToList()));
        }

        return await Task.FromResult(ret);
    }

    public List<SubstitutionWithPosition> GetSubstitutionWithPositionsForATeamInAGame(IEnumerable<(long PlayerId, PlayerPositions Position)> initialAlignment, IQueryable<Substitution> substitutions)
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

            positions[substitution.PlayerInId] = oldPosition;

            var playerInId = substitution.PlayerInId;

            ret.Add(new SubstitutionWithPosition()
            {
                PlayerInId = playerInId,
                PlayerOutId = playerOutId,
                Position = oldPosition,
                Time = substitution.Time
            }
            );
        }

        return ret;
    }

    public async Task<(bool status, string errorMessage)> CanAddSubstitution(long gameId, long teamId, Substitution newSubstitution)
    {
        var playerInSeries_table = unitOfWork.Repository<PlayerInSeries>().DbSet;
        var game_table = unitOfWork.Repository<Game>().DbSet;

        var gis = (
            from g in game_table
            where g.Id == gameId
            select g
        ).First();

        var playerInst = (
            from pis in playerInSeries_table
            where pis.PlayerId == newSubstitution.PlayerInId && pis.SeriesId == gis.SeriesId
            select pis
        ).First();

        if (playerInst.TeamId != teamId)
            return (false, "PlayerIn don't play in Team");
        
        var playerOutst = (
            from pis in playerInSeries_table
            where pis.PlayerId == newSubstitution.PlayerOutId && pis.SeriesId == gis.SeriesId
            select pis
        ).First();

        if (playerOutst.TeamId != teamId)
            return (false, "PlayerOut don't play in Team");

        var alignedPlayerInGame_table = unitOfWork.Repository<AlignedPlayerInGame>().DbSet;
        var playerInPosition_table = unitOfWork.Repository<PlayerInPosition>().DbSet;

        var alignment = (
        from apig in alignedPlayerInGame_table
        where apig.GameId == gameId && apig.TeamId == teamId
        select new
        {
            apig.PlayerId,
            apig.Position
        }).ToList().Select(x => (x.PlayerId, x.Position));

        Dictionary<long, PlayerPositions> isInAlignment = new();
        foreach (var apig in alignment)
        {
            isInAlignment.Add(apig.PlayerId, apig.Position);
        }

        var curSubstitutions = await GetSubstitutionsWithExtrasAsync(gameId, teamId);

        Dictionary<long, bool> isOutOfGame = new();
        var validSubstitution = (long playerIn, long playerOut) =>
        {
            if (isOutOfGame.ContainsKey(playerIn))
                return (false, "PlayerIn has already played in the game");

            if (!isInAlignment.ContainsKey(playerOut))
                return (false, "PlayerOut is not in lineup");

            var pipIn = (
                from pip in playerInPosition_table
                where pip.PlayerId == playerIn && pip.Position == isInAlignment[playerOut]
                select pip
            ).ToList();

            if (pipIn.IsNullOrEmpty())
                return (false, "PlayerIn can't play in that position");

            if (isInAlignment.ContainsKey(playerIn))
            {
                var pipOut = (
                    from pip in playerInPosition_table
                    where pip.PlayerId == playerOut && pip.Position == isInAlignment[playerIn]
                    select pip
                ).ToList();

                if (pipOut.IsNullOrEmpty())
                    return (false, "PlayerOut can't play in that position");

                (isInAlignment[playerIn], isInAlignment[playerOut]) = (isInAlignment[playerOut], isInAlignment[playerIn]);
            }
            else
            {
                isInAlignment.Add(playerIn, isInAlignment[playerOut]);
                isInAlignment.Remove(playerOut);
                isOutOfGame.Add(playerOut, true);
            }

            return (true, "OK");
        };

        bool flag = false;
        foreach (var substitution in curSubstitutions)
        {
            if (substitution.Time > newSubstitution.Time)
            {
                if (!flag)
                {
                    var state = validSubstitution(newSubstitution.PlayerInId, newSubstitution.PlayerOutId);
                    if (!state.Item1) return state;
                    flag = true;
                }

                var newState = validSubstitution(substitution.PlayerInId, substitution.PlayerOutId);
                if (!newState.Item1)
                    return (newState.Item1, "Substitution affects future substitutions " + newState.Item2);
            }
            else
            {
                if (isInAlignment.ContainsKey(substitution.PlayerInId))
                {
                    (isInAlignment[substitution.PlayerInId], isInAlignment[substitution.PlayerOutId]) = (isInAlignment[substitution.PlayerOutId], isInAlignment[substitution.PlayerInId]);
                }
                else
                {
                    isInAlignment.Add(substitution.PlayerInId, isInAlignment[substitution.PlayerOutId]);
                    isInAlignment.Remove(substitution.PlayerOutId);
                    isOutOfGame.Add(substitution.PlayerOutId, true);
                }
            }
        }

        if (!flag)
        {
            var state = validSubstitution(newSubstitution.PlayerInId, newSubstitution.PlayerOutId);
            if (!state.Item1) return state;
            flag = true;
        }

        return (true, "OK");
    }

    public async Task<(bool status, string errorMessage)> CanDeleteSubstitution(long gameId, long teamId, Substitution oldSubstitution)
    {
        var alignedPlayerInGame_table = unitOfWork.Repository<AlignedPlayerInGame>().DbSet;
        var playerInPosition_table = unitOfWork.Repository<PlayerInPosition>().DbSet;

        var alignment = (
        from apig in alignedPlayerInGame_table
        where apig.GameId == gameId && apig.TeamId == teamId
        select new
        {
            apig.PlayerId,
            apig.Position
        }).ToList().Select(x => (x.PlayerId, x.Position));

        Dictionary<long, PlayerPositions> isInAlignment = new();
        foreach (var apig in alignment)
        {
            isInAlignment.Add(apig.PlayerId, apig.Position);
        }

        var curSubstitutions = await GetSubstitutionsWithExtrasAsync(gameId, teamId);

        Dictionary<long, bool> isOutOfGame = new();
        var validSubstitution = (long playerIn, long playerOut) =>
        {
            if (isOutOfGame.ContainsKey(playerIn))
                return (false, "PlayerIn has already played in the game");

            if (!isInAlignment.ContainsKey(playerOut))
                return (false, "PlayerOut is not in lineup");

            var pipIn = (
                from pip in playerInPosition_table
                where pip.PlayerId == playerIn && pip.Position == isInAlignment[playerOut]
                select pip
            ).ToList();

            if (pipIn.IsNullOrEmpty())
                return (false, "PlayerIn can't play in that position");

            if (isInAlignment.ContainsKey(playerIn))
            {
                var pipOut = (
                    from pip in playerInPosition_table
                    where pip.PlayerId == playerOut && pip.Position == isInAlignment[playerIn]
                    select pip
                ).ToList();

                if (pipOut.IsNullOrEmpty())
                    return (false, "PlayerOut can't play in that position");

                (isInAlignment[playerIn], isInAlignment[playerOut]) = (isInAlignment[playerOut], isInAlignment[playerIn]);
            }
            else
            {
                isInAlignment.Add(playerIn, isInAlignment[playerOut]);
                isInAlignment.Remove(playerOut);
                isOutOfGame.Add(playerOut, true);
            }

            return (true, "OK");
        };

        bool found = false;
        foreach (var substitution in curSubstitutions)
        {
            if ((substitution.PlayerInId, substitution.PlayerOutId, substitution.Time) == (oldSubstitution.PlayerInId, oldSubstitution.PlayerOutId, oldSubstitution.Time))
            {
                found = true;
                continue;
            }

            var state = validSubstitution(substitution.PlayerInId, substitution.PlayerOutId);
            if (!state.Item1) return (state.Item1, "Can't be deleted " + state.Item2);
        }

        if (!found)
            return (false, "Substitution not found");

        return (true, "OK");
    }
}

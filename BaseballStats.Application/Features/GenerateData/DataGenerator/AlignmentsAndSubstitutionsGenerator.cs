using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator
{

    public static (List<AlignedPlayerInGame> alignedPlayerInGames, List<Substitution> substitutions) GenerateAlignmentsAndSubstitutions(List<Domain.Entities.Player> players, List<Domain.Entities.Game> games, List<Domain.Entities.PlayerInPosition> playerInPositions, List<Domain.Entities.PlayerInSeries> playerInSeries)
    {
        var answ = new List<AlignedPlayerInGame>();
        var substitutions = new List<Substitution>();

        var playersByPosition = (
            from pip in playerInPositions
            group pip.PlayerId by pip.Position into g
            select new { Position = g.Key, Players = g.ToHashSet() }
        ).ToDictionary(x => x.Position, x => x.Players);

        foreach(var game in games)
        {
            var team1Players = (
                from pis in playerInSeries
                where pis.SeriesId == game.SeriesId && pis.TeamId == game.Team1Id
                select pis.PlayerId
            ).ToList();

            var initialAlignment1 = new List<AlignedPlayerInGame>();
            var availablePlayers1 = team1Players.ToHashSet();
            GenerateInitialAlignment(game.Team1Id, game.Id, playersByPosition, ref availablePlayers1, ref initialAlignment1);

            var team2Players = (
                from pis in playerInSeries
                where pis.SeriesId == game.SeriesId && pis.TeamId == game.Team2Id
                select pis.PlayerId
            ).ToList();

            var initialAlignment2 = new List<AlignedPlayerInGame>();
            var availablePlayers2 = team2Players.ToHashSet();
            GenerateInitialAlignment(game.Team2Id, game.Id, playersByPosition, ref availablePlayers2, ref initialAlignment2);

            answ.AddRange(initialAlignment1);
            answ.AddRange(initialAlignment2);

            var team1Substitutions = GenerateSubstitutions(game.Id, game.Team1Id, initialAlignment1, playersByPosition, team1Players);
            var team2Substitutions = GenerateSubstitutions(game.Id, game.Team2Id, initialAlignment2, playersByPosition, team2Players);

            substitutions.AddRange(team1Substitutions);
            substitutions.AddRange(team2Substitutions);
        }

        return (answ, substitutions);
    }

    private static bool GenerateInitialAlignment(long teamId, long gameId, Dictionary<PlayerPositions, HashSet<long>> playersByPosition, ref HashSet<long> availablePlayers, ref List<AlignedPlayerInGame> answ, int positionIndex = 0)
    {
        if (positionIndex >= ValidPlayerPositions.Count)
        {
            return true;
        }

        var position = ValidPlayerPositions[positionIndex];
        var availablePlayersList = availablePlayers.ToArray();
        var random = new Random(RandomSeed);
        
        random.Shuffle(availablePlayersList);

        foreach(var player in availablePlayersList)
        {
            if (!playersByPosition[position].Contains(player)) continue;

            availablePlayers.Remove(player);
            answ.Add(new() {
                PlayerId = player,
                Position = position,
                GameId = gameId,
                TeamId = teamId
            });

            var success = GenerateInitialAlignment(teamId, gameId, playersByPosition, ref availablePlayers, ref answ, positionIndex + 1);

            availablePlayers.Add(player);

            if (success)
                return true;

            answ.RemoveAt(answ.Count - 1);
        }   

        return false;

    }

    private static List<Domain.Entities.Substitution> GenerateSubstitutions(long gameId, long teamId, List<AlignedPlayerInGame> initialAlignment, Dictionary<PlayerPositions, HashSet<long>> playersByPosition, List<long> teamPlayers)
    {
        const double gameLengthHours = 3;
        const double maxSubstitutionAttemptInterval = 3d/5;
        var answ = new List<Domain.Entities.Substitution>();

        var currentPositions = (
            from ia in initialAlignment
            select new { ia.PlayerId, ia.Position }
        ).ToDictionary(x => x.PlayerId, x => x.Position);

        var playersInGame = (
            from pip in currentPositions
            select pip.Key
        ).ToList();

        var playersNotInGame = teamPlayers.Except(playersInGame).ToList();

        var usedPlayers = playersInGame.ToHashSet();

        bool hasPlayed(long playerId) => usedPlayers.Contains(playerId);
        
        bool canPlay(long playerId, PlayerPositions position) => playersByPosition[position].Contains(playerId);

        TimeSpan hoursToTimeSpan(double hours) => new TimeSpan((long)(hours*3.6e+12));

        var random = new Random();
        var curTime = random.NextDouble()*maxSubstitutionAttemptInterval;

        while(curTime < gameLengthHours)
        {
            var pOutIndex = random.Next(0, playersInGame.Count);
            var playerOut = playersInGame[pOutIndex];

            if (random.Next(2) == 1)
            {
                var pInIndex = random.Next(0, playersInGame.Count);
                if (pInIndex == pOutIndex) continue;

                var playerIn = playersInGame[pInIndex];
                
                var pOutOldPosition = currentPositions[playerOut];
                var pInOldPosition = currentPositions[playerIn];

                if (canPlay(playerIn, pOutOldPosition) && canPlay(playerOut, pInOldPosition))
                {
                    currentPositions[playerIn] = pOutOldPosition;
                    currentPositions[playerOut] = pInOldPosition;

                    answ.Add(new Substitution()
                    {
                        TeamId = teamId,
                        GameId = gameId,
                        PlayerInId = playerIn,
                        PlayerOutId = playerOut,
                        Time = hoursToTimeSpan(curTime)
                    });
                }
            }
            else
            {
                if (playersNotInGame.Count == 0) continue;

                var pInIndex = random.Next(0, playersNotInGame.Count);

                var playerIn = playersNotInGame[pInIndex];

                if (hasPlayed(playerIn)) continue;

                var pOutPosition = currentPositions[playerOut];

                if (canPlay(playerIn, pOutPosition))
                {
                    playersNotInGame[pInIndex] = playerOut;
                    playersInGame[pOutIndex] = playerIn;

                    currentPositions.Remove(playerOut);
                    currentPositions[playerIn] = pOutPosition;

                    answ.Add(new Substitution()
                    {
                        TeamId = teamId,
                        GameId = gameId,
                        PlayerInId = playerIn,
                        PlayerOutId = playerOut,
                        Time = hoursToTimeSpan(curTime)
                    });
                }
            }

            curTime += random.NextDouble()*maxSubstitutionAttemptInterval;
        }
        
        return answ;
    }

}
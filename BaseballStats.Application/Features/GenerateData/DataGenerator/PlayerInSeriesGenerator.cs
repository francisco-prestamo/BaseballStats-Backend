using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator
{
    const long PlayersPerTeam = 25;
        
    private static List<Domain.Entities.PlayerInSeries> GeneratePlayerInSeries(List<Domain.Entities.Player> players, List<Domain.Entities.Series> series, List<Domain.Entities.Team> teams, List<Domain.Entities.PlayerInPosition> playerInPositions)
    {
        const long maxIterations = 10_000_000;

        Dictionary<PlayerPositions, HashSet<long>> allowedPositions = new();

        foreach(var pip in playerInPositions)
        {
            if(!allowedPositions.ContainsKey(pip.Position))
                allowedPositions[pip.Position] = new();
            allowedPositions[pip.Position].Add(pip.PlayerId);
        }

        long currIterations = 0;
        List<Domain.Entities.PlayerInSeries> answ = new();
        
        foreach(var s in series)
        {
            List<(long TeamId, HashSet<long> PlayerIds)> seriesAssignments;
            (currIterations, seriesAssignments) = AssignPlayersToTeamsInASeries(currIterations, maxIterations, players, teams, allowedPositions);

            foreach(var teamAssignment in seriesAssignments)
            {
                foreach(var playerId in teamAssignment.PlayerIds)
                {
                    answ.Add(new(){
                        PlayerId = playerId,
                        SeriesId = s.Id,
                        TeamId = teamAssignment.TeamId
                    });
                }
            }
        }

        return answ;
    }

    private static (long newIterations, List<(long TeamId, HashSet<long> PlayerIds)>) AssignPlayersToTeamsInASeries(long initialIterations, long maxIterations, List<Domain.Entities.Player> players, List<Domain.Entities.Team> teams, Dictionary<PlayerPositions, HashSet<long>> allowedPositions)
    {
        var availablePlayers = players.Select(x => x.Id).ToHashSet();
        var assignments = new List<(long teamId, HashSet<long> PlayerIds)>();
        var currIterations = initialIterations;
        bool success;
        foreach (var team in teams)
        {
            var teamPlayers = new HashSet<long>();

            (currIterations, success) = FillTeam(currIterations, maxIterations, allowedPositions, ref availablePlayers, ref teamPlayers);
            
            if (!success)
                return (currIterations, assignments);

            var extraPlayers = PickPlayers(ref availablePlayers, PlayersPerTeam - teamPlayers.Count());
            
            foreach(var playerId in extraPlayers) teamPlayers.Add(playerId);

            assignments.Add((team.Id, teamPlayers));

            if (currIterations >= maxIterations)
                return (currIterations, assignments);
        }

        return (currIterations, assignments);
    }

    private static (long newIterations, bool success) FillTeam(long initialIterations, long maxIterations, Dictionary<PlayerPositions, HashSet<long>> allowedPositions, ref HashSet<long> availablePlayers, ref HashSet<long> answer, int positionIndex = 0)
    {
        if (positionIndex >= ValidPlayerPositions.Count())
            return (initialIterations, true);

        var currIterations = initialIterations;
        bool success;
        var playersThatCanPlayPosition = allowedPositions[ValidPlayerPositions[positionIndex]].ToList();
        foreach(var player in playersThatCanPlayPosition)
        {
            currIterations++;
            
            if (currIterations >= maxIterations)
            {
                return (currIterations, false);
            }

            if (!availablePlayers.Contains(player)) continue;

            availablePlayers.Remove(player);
            answer.Add(player);
            
            (currIterations, success) = FillTeam(currIterations, maxIterations, allowedPositions, ref availablePlayers, ref answer, positionIndex + 1);
            

            if (success)
            {
                return (currIterations, success);
            }

            answer.Remove(player);
            availablePlayers.Add(player);

        }

        return (currIterations, false);
    }  

    private static List<long> PickPlayers(ref HashSet<long> availablePlayers, long amount)
    {
        var random = new Random(RandomSeed + 3);

        List<long> answ = [];
        var availablePlayersList = availablePlayers.ToList();
        availablePlayersList.Sort();

        for (int i = 0; i < amount; i++)
        {
            if (!availablePlayersList.Any()) break;

            var k = random.Next(0, availablePlayersList.Count());

            var chosen = availablePlayersList[k];

            availablePlayers.Remove(chosen);
            availablePlayersList.Remove(chosen);

            answ.Add(chosen);
        }

        return answ;
    }
}

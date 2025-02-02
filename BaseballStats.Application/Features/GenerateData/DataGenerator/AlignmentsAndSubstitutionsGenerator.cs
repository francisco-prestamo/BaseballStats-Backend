using System.Formats.Asn1;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator
{


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

    private static List<Domain.Entities.Substitution>

}
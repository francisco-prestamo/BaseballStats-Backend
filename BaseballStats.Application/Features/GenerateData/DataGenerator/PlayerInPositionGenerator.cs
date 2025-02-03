using BaseballStats.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator 
{
    public static List<Domain.Entities.PlayerInPosition> GeneratePlayerInPosition(int Amount, List<Domain.Entities.Player> players)
    {
        var random = new Random(RandomSeed + 9);
        var playerInPositions = new List<Domain.Entities.PlayerInPosition>();

        var playerSetWithAllPlayers = new HashSet<long>(players.Select(x => x.Id));
        int pos = 0;
        
        while(!playerSetWithAllPlayers.IsNullOrEmpty())
        {
            var playerId = playerSetWithAllPlayers.First();
            var position = ValidPlayerPositions[pos];
            
            var playerInPosition = new Domain.Entities.PlayerInPosition
            {
                PlayerId = playerId,
                Position = position
            };

            playerInPositions.Add(playerInPosition);
            playerSetWithAllPlayers.Remove(playerId);
            pos = (pos + 1) % ValidPlayerPositions.Count;
        }

        for (int i = 0; i < Amount; i++)
        {
            var playerId = players[random.Next(0, players.Count)].Id;
            var position = ValidPlayerPositions[random.Next(0, ValidPlayerPositions.Count)];
            
            var playerInPosition = new Domain.Entities.PlayerInPosition
            {
                PlayerId = playerId,
                Position = position
            };
            playerInPositions.Add(playerInPosition);
        }

        playerInPositions = (
            from pip in playerInPositions
            group pip by new { pip.PlayerId, pip.Position } into g
            select new Domain.Entities.PlayerInPosition
            {
                PlayerId = g.Key.PlayerId,
                Position = g.Key.Position,
                Effectiveness = random.NextDouble() * (1 - 0.5) + 0.5
            }).ToList();

        return playerInPositions;
    }
}
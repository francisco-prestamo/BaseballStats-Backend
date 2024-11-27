using BaseballStats.Domain.Enums;

namespace BaseballStats.Domain.Entities
{
    public class AlignedPlayerInGame : Entity
    {
        public long PlayerId { get; set; }
        public Player Player { get; set; } = null!;

        public long GameId { get; set; }
        public Game Game { get; set; } = null!;

        public long TeamId { get; set; }
        public Team Team { get; set; } = null!;

        public PlayerPositions Position { get; set; }
    }
}
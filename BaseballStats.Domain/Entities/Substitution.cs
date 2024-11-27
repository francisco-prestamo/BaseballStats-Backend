namespace BaseballStats.Domain.Entities;

public class Substitution : Entity
{
    public long PlayerInId { get; set; }
    public Player PlayerIn { get; set; } = null!;
    
    public long PlayerOutId { get; set; }
    public Player PlayerOut { get; set; } = null!;

    public long GameId { get; set; }
    public Game Game { get; set; } = null!;

    public long TeamId { get; set; }
    public Team Team { get; set; } = null!;

    public TimeSpan Time { get; set; }
} 
namespace BaseballStats.Domain.Entities;

public class Pitcher : Entity
{
    public long Id { get; set; }
    public Player Player { get; set; } = null!;
    public int GamesWonNumber { get; set; }
    public int GamesLostNumber { get; set; }
    public bool RightHanded { get; set; }
    public double AllowedRunsAvg { get; set; }
}
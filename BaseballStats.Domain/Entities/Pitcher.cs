namespace BaseballStats.Domain.Entities;

public class Pitcher : Player
{
    public int GamesWonNumber { get; set; }
    public int GamesLostNumber { get; set; }
    public bool RightHanded { get; set; }
    public double AllowedRunsAvg { get; set; }
}
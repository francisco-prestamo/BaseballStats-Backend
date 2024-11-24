namespace BaseballStats.Domain.Entities;

public class PlayerInPosition : Entity
{
    public required long PlayerId { get; set; } // primary key attribute 1
    public Player Player { get; set; } = null!;

    public required string Position { get; set; } // primary key attribute 2

    public double Effectiveness { get; set; }
}
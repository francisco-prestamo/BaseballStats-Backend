namespace BaseballStats.Domain.Entities;

public class PlayerInSeries : Entity
{
    public required long PlayerId { get; set; } // primary key attribute 1;
    public Player Player { get; set; } = null!;

    public required long SeriesId { get; set; } // primary key attribute 2;
    public Series Series { get; set; } = null!;

    public long? TeamId { get; set; }
    public Team? Team { get; set; }
}
using BaseballStats.Domain.Enums;

namespace BaseballStats.Domain.Entities;

public class StarPlayerInPosition : Entity
{
    public required long PlayerId { get; set; } // primary key attribute 1
    public required PlayerPositions Position { get; set; } // primary key attribute 2
    public PlayerInPosition PlayerInPosition { get; set; } = null!;

    public required long SeriesId { get; set; } // primary key attribute 3
    public Series Series { get; set; } = null!;
}
using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.ResultSets;

public class SubstitutionWithPosition
{
    public long PlayerInId { get; set; }
    public long PlayerOutId { get; set; }
    public PlayerPositions Position { get; set; }
    public TimeSpan Time { get; set; }
}
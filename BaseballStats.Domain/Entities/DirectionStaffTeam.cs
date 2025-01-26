namespace BaseballStats.Domain.Entities;

public class DirectionStaffTeam : Entity
{
    public long DirectionStaffId { get; set; }
    public DirectionStaff DirectionStaff { get; set; } = null!;

    public long TeamId { get; set; }
    public Team Team { get; set; } = null!;
}
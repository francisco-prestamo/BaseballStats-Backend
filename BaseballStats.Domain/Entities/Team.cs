using BaseballStats.Domain.Entities.Identity;

namespace BaseballStats.Domain.Entities;

public class Team : Entity<long>
{
    public string Name { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string RepresentedEntity { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;

    // Technical Director Relation
    public long TechnicalDirectorId { get; set; }
    public TechnicalDirector TechnicalDirector { get; set; } = null!;

    // Direction Staff Relation
    public List<DirectionStaff> DirectionStaffs = [];
}
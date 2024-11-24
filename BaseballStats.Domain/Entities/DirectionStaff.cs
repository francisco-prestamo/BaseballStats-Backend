namespace BaseballStats.Domain.Entities;

public class DirectionStaff : Entity<long>
{
    public string Name { get; set; } = string.Empty;

    // Teams that the staff is part of
    public List<Team> TeamsLead = [];
}
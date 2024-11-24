namespace BaseballStats.Domain.Entities;

public class Series : Entity<long>
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    // Season Relation
    public required long SeasonId { get; set; }
    public Season Season { get; set; } = null!;
}
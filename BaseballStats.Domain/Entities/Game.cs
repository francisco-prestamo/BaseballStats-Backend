namespace BaseballStats.Domain.Entities;

public class Game : Entity<long>
{
    public required long Team1Id { get; set; }
    public Team Team1 { get; set; } = null!;

    public required long Team2Id { get; set; }
    public Team Team2 { get; set; } = null!;

    public required DateOnly Date { get; set; }

    public bool Winner1 { get; set; }
    public int Runs1 { get; set; }
    public int Runs2 { get; set; }

    // Series Relation
    public long SeriesId { get; set; }
    public Series Series { get; set; } = null!;
}
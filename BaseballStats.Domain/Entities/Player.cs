namespace BaseballStats.Domain.Entities;

public class Player : Entity<long>
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public int YearsOfExperience { get; set; }
    public double? BattingAverage { get; set; }
}
namespace BaseballStats.Application.DTOs;

public record RegularPlayerDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public int YearsOfExperience { get; set; }
    public double? BattingAverage { get; set; }
}
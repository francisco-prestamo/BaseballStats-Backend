namespace BaseballStats.Application.DTOs;

public record SeriesDto
{
    public long Id { get; init; }

    public long IdSeason { get; init; }

    public string Name { get; init; } = null!;
    public  string Type { get; init; } = null!;

    public DateOnly StartDate { get; init; }

    public DateOnly EndDate { get; init; }


}
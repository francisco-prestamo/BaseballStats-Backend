namespace BaseballStats.Application.DTOs;

public record SeriesDto
{
    public string Name { get; init; } = null!;
    public  string Type { get; init; } = null!;

    public DateOnly StartDate { get; init; }

    public DateOnly EndDate { get; init; }

    public long SeasonId { get; init; }

}
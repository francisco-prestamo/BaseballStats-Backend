namespace BaseballStats.Application.DTOs;

public record SeriesDto
{
    public long id { get; init; }

    public long idSeason { get; init; }

    public string name { get; init; } = null!;
    public  string type { get; init; } = null!;

    public DateOnly startDate { get; init; }

    public DateOnly sndDate { get; init; }


}
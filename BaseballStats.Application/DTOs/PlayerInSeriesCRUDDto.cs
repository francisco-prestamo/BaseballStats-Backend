namespace BaseballStats.Application.DTOs;

public record PlayerInSeriesCRUDDto
{
    public long PlayerId { get; init; }
    public long SerieId { get; init; }
    public long SeasonId { get; init; }
    public long TeamId { get; init; }
}
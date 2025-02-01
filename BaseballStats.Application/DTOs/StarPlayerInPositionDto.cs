using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.DTOs;

public class StarPlayerInPositionDto
{
   public long PlayerId { get; init; }
   public string Position { get; init; } = null!;
   public long SeriesId { get; init; }
   public long SeasonId { get; init; }
}


namespace BaseballStats.Application.DTOs;

public record SingleAlignmentDto
{
  public long TeamId { get; init; }
  public long GameId { get; init; }
  public List<PlayerInPositionDto> PlayersInPosition { get; init; } = null!;

}
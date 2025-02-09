using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.StarPlayerInPosition.Delete;

public class DeleteStarPlayerInPositionCommand : ICommand<StarPlayerInPositionDto>
{
    public long PlayerId { get; set; }
    public long SeriesId { get; set; }
    public long SeasonId { get; set; }
    public string Position { get; set; } = null!;
}

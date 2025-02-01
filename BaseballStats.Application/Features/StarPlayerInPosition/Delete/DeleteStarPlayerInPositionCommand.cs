using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.StarPlayerInPosition.Delete;

public class DeleteStarPlayerInPositionCommand : ICommand<StarPlayerInPositionDto>
{
    public int PlayerId { get; set; }
    public int SeriesId { get; set; }
    public int SeasonId { get; set; }
    public string Position { get; set; } = null!;
}

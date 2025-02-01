using FastEndpoints;
using BaseballStats.Application.DTOs;

namespace BaseballStats.Application.Features.StarPlayerInPosition.Post;


public class PostStarPlayerInPositionCommand : ICommand<StarPlayerInPositionDto>
{
    public long PlayerId { get; set; }
    public string Position { get; set; } = null!;
    public long SeriesId { get; set; }
    public long SeasonId { get; set; }
}


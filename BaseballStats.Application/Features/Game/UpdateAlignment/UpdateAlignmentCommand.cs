using BaseballStats.Application.DTOs;
using BaseballStats.Application.ResultSets;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.UpdateAlignment
{
    public class UpdateAlignmentCommand :  ICommand<SingleAlignmentDto>
    {
        public long GameId { get; set; }
        public long TeamId { get; set; }
        public List<PlayerInPositionDto> Alignment { get; set; } = null!;
    }
}
using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.GetAlignmentFromTeam;

public record GetAlignmentFromTeamCommand : ICommand<List<PlayerInPositionDto>>
{
    public long TeamId { get; init; }
    public long GameId { get; init; }
}
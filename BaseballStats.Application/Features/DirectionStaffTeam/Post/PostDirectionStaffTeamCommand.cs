using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.DirectionStaffTeam.Post;

public record PostDirectionStaffTeamCommand : ICommand<DirectionStaffTeamDto>
{
    public long TeamId { get; set; }
    public long DirectionMemberId { get; set; }
}
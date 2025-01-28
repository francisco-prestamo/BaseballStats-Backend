using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.DirectionStaffTeamNamespace.Post;

public record PostDirectionStaffTeamCommand : ICommand<DirectionStaffTeamDto>
{
    public long TeamId { get; set; }
    public long DirectionMemberId { get; set; }
}
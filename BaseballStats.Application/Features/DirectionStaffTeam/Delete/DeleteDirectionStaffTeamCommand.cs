using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.DirectionStaffTeam.Delete;

// ReSharper disable once ClassNeverInstantiated.Global
public record DeleteDirectionStaffTeamCommand : ICommand<DirectionStaffTeamDto>
{
    public long TeamId { get; init; }
    public long DirectionMemberId { get; init; }
}
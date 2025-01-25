using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.DirectionStaffTeam.Get;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetDirectionStaffTeamCommand : ICommand<List<DirectionStaffTeamDto>>
{
}
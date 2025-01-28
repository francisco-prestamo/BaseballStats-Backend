using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.DirectionStaffTeamNamespace.Get;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetDirectionStaffTeamCommand : ICommand<List<DirectionStaffTeamDto>>
{
}
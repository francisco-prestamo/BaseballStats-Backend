using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Team.Get;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetTeamCommand : ICommand<TeamDto>
{
    public long TeamId { get; init; }
}
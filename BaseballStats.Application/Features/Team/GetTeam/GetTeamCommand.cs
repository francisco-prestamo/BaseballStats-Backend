using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.TeamNamespace.GetTeam;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetTeamCommand : ICommand<TeamDto>
{
    public long SeasonId { get; init; }
    public long SeriesId { get; init; }
    public long TeamId { get; init; }
}
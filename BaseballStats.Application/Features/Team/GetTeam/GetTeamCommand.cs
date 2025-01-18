using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Team.GetTeam;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetTeamCommand : ICommand<TeamDto>
{
    public long SeasonId { get; init; }
    public long SeriesId { get; init; }
    public long TeamId { get; init; }
}
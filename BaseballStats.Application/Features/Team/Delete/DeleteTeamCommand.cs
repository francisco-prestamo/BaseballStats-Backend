using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Team.Delete;

public record DeleteTeamCommand : ICommand<TeamDto>
{
    public long Id { get; init; }
}
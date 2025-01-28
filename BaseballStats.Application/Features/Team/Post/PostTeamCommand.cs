using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Team.Post;

public record PostTeamCommand : ICommand<TeamDto>
{
    public string Name { get; init; } = null!;
    public string RepresentedEntity { get; init; } = null!;
    public string Initials { get; init; } = null!;
    public string Color { get; init; } = null!;
    public long DtId { get; init; }
}
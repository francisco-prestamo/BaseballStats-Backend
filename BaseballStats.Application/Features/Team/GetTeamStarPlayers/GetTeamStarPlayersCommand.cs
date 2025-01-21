using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.TeamNamespace.GetTeamStarPlayers;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetTeamStarPlayersCommand : ICommand<List<PlayerInPositionDto>>
{
    public long SeasonId { get; init; }
    public long SeriesId { get; init; }
    public long TeamId { get; init; }
}
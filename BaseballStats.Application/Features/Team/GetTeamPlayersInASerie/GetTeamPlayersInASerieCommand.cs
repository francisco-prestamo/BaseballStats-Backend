using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Team.GetTeamPlayersInASerie;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetTeamPlayersInASerieCommand : ICommand<List<RegularPlayerDto>>
{
    public long SeasonId { get; init; }
    public long SeriesId { get; init; }
    public long TeamId { get; init; }
}
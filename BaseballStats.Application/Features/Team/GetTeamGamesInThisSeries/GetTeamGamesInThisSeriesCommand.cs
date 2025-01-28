using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Team.GetTeamGamesInThisSeries;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetTeamGamesInThisSeriesCommand : ICommand<List<GameWithTeamsDto>>
{
    public long SeasonId { get; init; }
    public long SeriesId { get; init; }
    public long TeamId { get; init; }
}
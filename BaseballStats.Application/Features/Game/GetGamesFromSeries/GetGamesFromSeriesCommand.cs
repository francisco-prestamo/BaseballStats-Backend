using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Game.GetGamesFromSeries;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetGamesFromSeriesCommand : ICommand<List<GameWithTeamsDto>>
{
    public long SeasonId { get; init; }
    public long SeriesId { get; init; }
}
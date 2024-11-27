using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Season.GetSeriesFromSeason;

public record GetSeriesFromSeasonCommand : ICommand<List<SeriesDto>>
{
    public long SeasonId { get; init; }
    
}
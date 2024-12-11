using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Series.GetTeamsFromSeries;

// ReSharper disable once ClassNeverInstantiated.Global
public record GetTeamsFromSeriesCommand : ICommand<List<TeamWithExtrasDto>>
{
    public long SeasonId { get; init; }
    public long SeriesId { get; init; }
}
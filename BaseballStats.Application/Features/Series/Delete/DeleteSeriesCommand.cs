using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Series.Delete;

public record DeleteSeriesCommand : ICommand<SeriesDto>
{
    public long SeasonId { get; init; }
    public long Id { get; init; }
}
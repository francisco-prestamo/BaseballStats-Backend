using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Series.Get;

public class GetSerieCommand : ICommand<SeriesDto>
{
    public long SeasonId { get; init; }
    public long Id { get; init; }
}
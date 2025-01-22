using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Series.Get;

public class GetSerieCommand : ICommand<SeriesDto>
{
    public long Id { get; init; }
}
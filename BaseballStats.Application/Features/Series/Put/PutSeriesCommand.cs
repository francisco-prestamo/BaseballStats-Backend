using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.Series.Put;

public record PutSeriesCommand : SeriesDto, ICommand<SeriesDto>
{
   public long SeasonId { get; init; }
   public long SerieId { get; init; }
}
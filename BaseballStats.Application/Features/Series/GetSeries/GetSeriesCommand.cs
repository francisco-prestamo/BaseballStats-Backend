using FastEndpoints;
using BaseballStats.Application.DTOs;

namespace BaseballStats.Application.Features.Series.GetSeries;

public class GetSeriesCommand : ICommand<List<SeriesDto>>
{
}
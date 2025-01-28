using BaseballStats.Application.DTOs;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInSeries.GetAll;

public record GetAllPlayerInSeriesCommand : ICommand<List<PlayerInSeriesCRUDDto>>
{
}
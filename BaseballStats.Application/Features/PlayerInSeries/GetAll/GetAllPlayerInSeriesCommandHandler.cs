using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.PlayerInSeries.GetAll;

public class GetAllPlayerInSeriesCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetAllPlayerInSeriesCommand, List<PlayerInSeriesCRUDDto>>
{
    public override Task<List<PlayerInSeriesCRUDDto>> ExecuteAsync(GetAllPlayerInSeriesCommand command, CancellationToken ct = default)
    {
        var playerInSeries_table = unitOfWork.Repository<Domain.Entities.PlayerInSeries>().DbSet;
        var series_table = unitOfWork.Repository<Domain.Entities.Series>().DbSet;

        var result = (
            from pis in playerInSeries_table
            join s in series_table on pis.SeriesId equals s.Id
            select pis.ToCRUDDto(s.SeasonId)
        ).ToList();

        return Task.FromResult(result);
    }
}
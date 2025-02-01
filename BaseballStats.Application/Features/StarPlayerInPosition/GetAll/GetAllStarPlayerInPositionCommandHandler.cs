using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.StarPlayerInPosition.GetAll;

public class GetAllStarPlayerInPositionCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetAllStarPlayerInPositionCommand, List<StarPlayerInPositionDto>>
{
    public override Task<List<StarPlayerInPositionDto>> ExecuteAsync(GetAllStarPlayerInPositionCommand command, CancellationToken ct = default)
    {
        var starPlayerInPosition_table = unitOfWork.Repository<Domain.Entities.StarPlayerInPosition>().DbSet;
        var series_table = unitOfWork.Repository<Domain.Entities.Series>().DbSet;

        var result = (
            from spip in starPlayerInPosition_table
            join s in series_table on spip.SeriesId equals s.Id
            select spip.ToDto(s.SeasonId)
        ).ToList();

        return Task.FromResult(result);
    }
}
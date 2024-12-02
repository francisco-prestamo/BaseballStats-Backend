using System.Data;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.Series.GetSeries;

public class GetSeriesCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetSeriesCommand, List<SeriesDto>>
{
    public override async Task<List<SeriesDto>> ExecuteAsync(GetSeriesCommand command, CancellationToken ct = default)
    {
        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();

        var series = await seriesRepository.GetAllAsync();

        var seriesDto = series.Select(x => x.ToDto());

        return seriesDto.ToList();
    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Series.Put;

public class PutSeriesCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PutSeriesCommand, SeriesDto>
{
    public override async Task<SeriesDto> ExecuteAsync(PutSeriesCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidation(command);

        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();

        var series = await seriesRepository.GetByIdAsync(command.Id);

        series!.Name = command.Name;
        series.Type = command.Type;
        series.StartDate = command.StartDate;
        series.EndDate = command.EndDate;
        series.SeasonId = command.SeasonId;

        series = await seriesRepository.UpdateAsync(series);

        return series.ToDto();
    }

    private async Task DatabaseValidation(PutSeriesCommand command)
    {
        var series = await unitOfWork.Repository<Domain.Entities.Series>().GetByIdAsync(command.Id);

        if (series == null)
            ThrowError("Series not found", StatusCodes.Status404NotFound);
        
        if(series.SeasonId != command.SeasonId)
            ThrowError("Series not found in the season", StatusCodes.Status404NotFound);

        var season = await unitOfWork.Repository<Domain.Entities.Season>().GetByIdAsync(command.SeasonId);

        if (season == null)
            ThrowError("Season not found", StatusCodes.Status400BadRequest);
    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.Series.Delete;

public class DeleteSeriesCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<DeleteSeriesCommand, SeriesDto>
{
    public override async Task<SeriesDto> ExecuteAsync(DeleteSeriesCommand command, CancellationToken ct = new CancellationToken())
    {
        await ValidateAsync(command);
        
        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();
        
        var serie = await seriesRepository.DeleteAsync(command.Id);
        
        await unitOfWork.SaveChangesAsync(ct);
        
        return serie!.ToDto();
    }

    private async Task ValidateAsync(DeleteSeriesCommand command)
    {
        var series = await unitOfWork.Repository<Domain.Entities.Series>().GetByIdAsync(command.Id);

        if (series == null)
            ThrowError("Series not found", 404);

        if (series.SeasonId != command.SeasonId)
            ThrowError("Series not found in season", 404);
    }
}
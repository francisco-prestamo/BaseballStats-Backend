using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Series.Get;

public class GetSerieCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetSerieCommand, SeriesDto>
{
    public override async Task<SeriesDto> ExecuteAsync(GetSerieCommand command, CancellationToken ct = new CancellationToken())
    {
        await ValidateAsync(command);

        var serie = await unitOfWork.Repository<Domain.Entities.Series>().GetByIdAsync(command.Id);

        return serie!.ToDto();
    }

    private async Task ValidateAsync(GetSerieCommand command)
    {
        var serie = await unitOfWork.Repository<Domain.Entities.Series>().GetByIdAsync(command.Id);

        if (serie == null)
            ThrowError("Serie not found", StatusCodes.Status404NotFound);
        
        if (serie.SeasonId != command.SeasonId)
            ThrowError("Serie not found in the season", StatusCodes.Status404NotFound);
    }
}
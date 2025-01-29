using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Season.Put;

public class PutSeasonCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PutSeasonCommand, SeasonDto>
{
    public override async Task<SeasonDto> ExecuteAsync(PutSeasonCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidationsAsync(command);

        await unitOfWork.Repository<Domain.Entities.Season>().DeleteAsync(command.SeasonId);
        var season = await unitOfWork.Repository<Domain.Entities.Season>().AddAsync(new Domain.Entities.Season() { Id = command.Id });

        await unitOfWork.SaveChangesAsync(ct);

        return season.ToDto();
    }

    private async Task DatabaseValidationsAsync(PutSeasonCommand command)
    {
        var season = await unitOfWork.Repository<Domain.Entities.Season>().GetByIdAsync(command.SeasonId);

        if (season == null)
            ThrowError("Season not found.", StatusCodes.Status404NotFound);

        season = await unitOfWork.Repository<Domain.Entities.Season>().GetByIdAsync(command.Id);

        if (season is not null)
            ThrowError($"Season {command.Id} already exists.", StatusCodes.Status400BadRequest);
    }
}
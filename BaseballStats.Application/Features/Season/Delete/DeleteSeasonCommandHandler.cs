using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Season.Delete;

public class DeleteSeasonCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<DeleteSeasonCommand, SeasonDto>
{
    public override async Task<SeasonDto> ExecuteAsync(DeleteSeasonCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidationsAsync(command);

        var seasonRepository = unitOfWork.Repository<Domain.Entities.Season>();

        var season = await seasonRepository.DeleteAsync(command.Id);
        
        await unitOfWork.SaveChangesAsync(ct);

        return season!.ToDto();
    }

    private async Task DatabaseValidationsAsync(DeleteSeasonCommand command)
    {
        var season = await unitOfWork.Repository<Domain.Entities.Season>().GetByIdAsync(command.Id);

        if (season == null)
            ThrowError("Season not found.", StatusCodes.Status404NotFound);
    }
}
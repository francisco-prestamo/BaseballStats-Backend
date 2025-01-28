using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Game.Delete;

public class DeleteGameCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<DeleteGameCommand, GameDto>
{
    public override async Task<GameDto> ExecuteAsync(DeleteGameCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidation(command);
        
        var game = await unitOfWork.Repository<Domain.Entities.Game>().DeleteAsync(command.GameId);
        var seasonId = (
            from series in unitOfWork.Repository<Domain.Entities.Series>().DbSet
            where series.Id == game!.SeriesId
            select series.SeasonId
        ).Single();
        
        await unitOfWork.SaveChangesAsync(ct);

        return game!.ToGameDto(seasonId);
    }

    private async Task DatabaseValidation(DeleteGameCommand command)
    {
        var game = await unitOfWork.Repository<Domain.Entities.Game>().GetByIdAsync(command.GameId);

        if (game == null)
            ThrowError("Game not found", StatusCodes.Status404NotFound);
    }
}
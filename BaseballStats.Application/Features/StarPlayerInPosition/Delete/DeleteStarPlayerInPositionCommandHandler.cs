using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.StarPlayerInPosition.Delete;

public class DeleteStarPlayerInPositionCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<DeleteStarPlayerInPositionCommand, StarPlayerInPositionDto>
{
    public override async Task<StarPlayerInPositionDto> ExecuteAsync(DeleteStarPlayerInPositionCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var starPlayerInPosition_table = unitOfWork.Repository<Domain.Entities.StarPlayerInPosition>().DbSet;
        var entity = (
            from spip in starPlayerInPosition_table
            where spip.PlayerId == command.PlayerId && spip.SeriesId == command.SeriesId && spip.Position == command.Position.GetPlayerPosition()
            select spip
        ).Single();

        starPlayerInPosition_table.Remove(entity);

        await unitOfWork.SaveChangesAsync(ct);

        return entity.ToDto(command.SeasonId);
    }

    private async Task DatabaseValidations(DeleteStarPlayerInPositionCommand command)
    {
        var player = await unitOfWork.Repository<Domain.Entities.Player>().GetByIdAsync(command.PlayerId);
        if (player == null)
        {
            ThrowError("Player not found", StatusCodes.Status404NotFound);
        }

        var series = await unitOfWork.Repository<Domain.Entities.Series>().GetByIdAsync(command.SeriesId);
        if (series == null)
        {
            ThrowError("Series not found", StatusCodes.Status404NotFound);
        }

        var season = await unitOfWork.Repository<Domain.Entities.Season>().GetByIdAsync(command.SeasonId);
        if (season == null)
        {
            ThrowError("Season not found", StatusCodes.Status404NotFound);
        }

        if (series.SeasonId != season.Id)
        {
            ThrowError("Series not found", StatusCodes.Status404NotFound);
        }
    }
}

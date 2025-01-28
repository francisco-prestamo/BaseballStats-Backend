using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Application.Services;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.PlayerInSeries.Delete;

public class DeletePlayerInSeriesCommandHandler(IUnitOfWork unitOfWork, ValidatePlayerInSeriesChangeService validatePlayerInSeriesChangeService) : CommandHandler<DeletePlayerInSeriesCommand, PlayerInSeriesCRUDDto>
{
    public override async Task<PlayerInSeriesCRUDDto> ExecuteAsync(DeletePlayerInSeriesCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var playerInSeries_table = unitOfWork.Repository<Domain.Entities.PlayerInSeries>().DbSet;
        var entity = (
            from pis in playerInSeries_table
            where pis.PlayerId == command.PlayerId && pis.SeriesId == command.SerieId
            select pis
        ).Single();

        playerInSeries_table.Remove(entity);

        await unitOfWork.SaveChangesAsync(ct);

        return entity.ToCRUDDto(command.SeasonId);
    }

    public async Task DatabaseValidations(DeletePlayerInSeriesCommand command)
    {
        var season = await unitOfWork.Repository<Domain.Entities.Season>().GetByIdAsync(command.SeasonId);

        if (season is null)
            ThrowError("Season not found", StatusCodes.Status404NotFound);

        var serie = await unitOfWork.Repository<Domain.Entities.Series>().GetByIdAsync(command.SerieId);

        if (serie is null)
            ThrowError("Serie not found", StatusCodes.Status404NotFound);

        var player = await unitOfWork.Repository<Domain.Entities.Player>().GetByIdAsync(command.PlayerId);

        if (player is null)
            ThrowError("Player not found", StatusCodes.Status404NotFound);

        var playerInSeries_table = unitOfWork.Repository<Domain.Entities.PlayerInSeries>().DbSet;
        var playerInSeries = (
            from pis in playerInSeries_table
            where pis.PlayerId == command.PlayerId && pis.SeriesId == command.SerieId
            select pis
        ).FirstOrDefault();

        if (playerInSeries is null)
            ThrowError("Player not assigned to a team in provided series", StatusCodes.Status404NotFound);

        var (isValid, errorMessage) = validatePlayerInSeriesChangeService.CanUnassignPlayerFromCurrentTeamInSeries(command.PlayerId, command.SerieId);

        if (!isValid)
            ThrowError(errorMessage, StatusCodes.Status400BadRequest);
    }
}
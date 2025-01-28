using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using BaseballStats.Application.Services;

namespace BaseballStats.Application.Features.PlayerInSeries.Put;

public class PutPlayerInSeriesCommandHandler(IUnitOfWork unitOfWork, ValidatePlayerInSeriesChangeService validatePlayerInSeriesChangeService) : CommandHandler<PutPlayerInSeriesCommand, PlayerInSeriesCRUDDto>
{
    public override async Task<PlayerInSeriesCRUDDto> ExecuteAsync(PutPlayerInSeriesCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var playerInSeries_table = unitOfWork.Repository<Domain.Entities.PlayerInSeries>().DbSet;
        var entity = (
            from pis in playerInSeries_table
            where pis.PlayerId == command.PlayerId && pis.SeriesId == command.SerieId
            select pis
        ).Single();

        entity.TeamId = command.TeamId;

        await unitOfWork.SaveChangesAsync(ct);

        return entity.ToCRUDDto(command.SeasonId);
    }

    private async Task DatabaseValidations(PutPlayerInSeriesCommand command)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = await playerRepository.GetByIdAsync(command.PlayerId);

        if (player is null)
            ThrowError("Player not found", StatusCodes.Status404NotFound);

        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();
        var series = await seriesRepository.GetByIdAsync(command.SerieId);

        if (series is null)
            ThrowError("Series not found", StatusCodes.Status404NotFound);

        var seasonRepository = unitOfWork.Repository<Domain.Entities.Season>();
        var season = await seasonRepository.GetByIdAsync(command.SeasonId);

        if (season is null)
            ThrowError("Season not found", StatusCodes.Status404NotFound);

        var playerInSeries_table = unitOfWork.Repository<Domain.Entities.PlayerInSeries>().DbSet;
        var previousAssignment = (
            from pis in playerInSeries_table
            where pis.PlayerId == command.PlayerId && pis.SeriesId == command.SerieId
            select pis
        ).FirstOrDefault();

        if (previousAssignment is null)
            ThrowError("Player not assigned to a team in provided series", StatusCodes.Status404NotFound);

        var (isValid, errorMessage) = validatePlayerInSeriesChangeService.CanUnassignPlayerFromCurrentTeamInSeries(command.PlayerId, command.SerieId);

        if (!isValid)
            ThrowError(errorMessage, StatusCodes.Status400BadRequest);
    }


}
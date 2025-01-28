using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.PlayerInSeries.Get;

public class GetPlayerInSeriesCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetPlayerInSeriesCommand, PlayerInSeriesCRUDDto>
{
    public override async Task<PlayerInSeriesCRUDDto> ExecuteAsync(GetPlayerInSeriesCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var playerInSeries_table = unitOfWork.Repository<Domain.Entities.PlayerInSeries>().DbSet;

        var entity = (
            from pis in playerInSeries_table
            where pis.PlayerId == command.PlayerId && pis.SeriesId == command.SerieId
            select pis
        ).Single();

        return entity.ToCRUDDto(command.SeasonId);
    }

    private async Task DatabaseValidations(GetPlayerInSeriesCommand command)
    {
        var season = await unitOfWork.Repository<Domain.Entities.Season>().GetByIdAsync(command.SeasonId);
        if (season is null)
            ThrowError("Season not found", StatusCodes.Status404NotFound);

        var series = await unitOfWork.Repository<Domain.Entities.Series>().GetByIdAsync(command.SerieId);
        if (series is null)
            ThrowError("Series not found", StatusCodes.Status404NotFound);

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
    }
    
}
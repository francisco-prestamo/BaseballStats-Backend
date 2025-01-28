using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.PlayerInSeries.Post;

public class PostPlayerInPositionCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostPlayerInSeriesCommand, PlayerInSeriesCRUDDto>
{
    public override async Task<PlayerInSeriesCRUDDto> ExecuteAsync(PostPlayerInSeriesCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var playerInSeriesRepository = unitOfWork.Repository<Domain.Entities.PlayerInSeries>();

        var entity = new Domain.Entities.PlayerInSeries()
        {
            PlayerId = command.PlayerId,
            SeriesId = command.SerieId,
            TeamId = command.TeamId
        };

        await playerInSeriesRepository.AddAsync(entity);

        await unitOfWork.SaveChangesAsync(ct);

        return entity.ToCRUDDto(command.SeasonId);
    }

    private async Task DatabaseValidations(PostPlayerInSeriesCommand command)
    {
        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();
        var series = await seriesRepository.GetByIdAsync(command.SerieId);

        if (series is null)
            ThrowError("Series not found", StatusCodes.Status404NotFound);

        var seasonRepository = unitOfWork.Repository<Domain.Entities.Season>();
        var season = await seasonRepository.GetByIdAsync(command.SeasonId);

        if (season is null)
            ThrowError("Season not found", StatusCodes.Status404NotFound);

        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = await playerRepository.GetByIdAsync(command.PlayerId);

        if (player is null)
            ThrowError("Player not found", StatusCodes.Status404NotFound);

        var playerInSeries_table = unitOfWork.Repository<Domain.Entities.PlayerInSeries>().DbSet;
        var entity = (
            from pis in playerInSeries_table
            where pis.PlayerId == command.PlayerId && pis.SeriesId == command.SerieId
            select pis
        ).FirstOrDefault();

        if (entity is not null)
            ThrowError("Player already assigned to team in provided series", StatusCodes.Status400BadRequest);        
    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.StarPlayerInPosition.Post;

public class PostStarPlayerInPositionCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostStarPlayerInPositionCommand, StarPlayerInPositionDto>
{
    public override async Task<StarPlayerInPositionDto> ExecuteAsync(PostStarPlayerInPositionCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);
        

        var starPlayerInPosition = new Domain.Entities.StarPlayerInPosition()
        {
            PlayerId = command.PlayerId,
            Position = command.Position.GetPlayerPosition(),
            SeriesId = command.SeriesId
        };

        await unitOfWork.Repository<Domain.Entities.StarPlayerInPosition>().AddAsync(starPlayerInPosition);

        await unitOfWork.SaveChangesAsync(ct);

        return starPlayerInPosition.ToDto(command.SeasonId);

    } 

    private async Task DatabaseValidations(PostStarPlayerInPositionCommand command)
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

        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;
        var canPlayerPlayPosition = (
            from pip in playerInPosition_table
            where pip.PlayerId == command.PlayerId && pip.Position == command.Position.GetPlayerPosition()
            select pip.PlayerId
        ).Any();

        if (!canPlayerPlayPosition)
        {
            ThrowError("Player cannot play in provided position", StatusCodes.Status400BadRequest);
        }
    }

}




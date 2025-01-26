using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Enums;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BaseballStats.Application.Features.PlayerInPosition.Put;

public class PutPlayerInPositionCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PutPlayerInPositionCommand, PlayerInPositionCRUDDto>
{

    public override async Task<PlayerInPositionCRUDDto> ExecuteAsync(PutPlayerInPositionCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;
        
        var entity = (
            from pip in playerInPosition_table
            where pip.PlayerId == command.PlayerId && pip.Position == command.Position.GetPlayerPosition()
            select pip
        ).Single();
        
        entity.Effectiveness = command.Effectiveness;

        var pepelfeo = new Domain.Entities.PlayerInPosition()
        {
            PlayerId = entity.PlayerId,
            Position = PlayerPositions.DesignatedHitter,
            Effectiveness = entity.Effectiveness
        };

        await unitOfWork.SaveChangesAsync(ct);

        return new()
        {
            PlayerId = command.PlayerId,
            Position = entity.Position.GetDisplayName(),
            Effectiveness = entity.Effectiveness
        };
    }

    private async Task DatabaseValidations(PutPlayerInPositionCommand command)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = await playerRepository.GetByIdAsync(command.PlayerId);

        if (player is null)
            ThrowError("Player not found", StatusCodes.Status404NotFound);


        var playerInPostion_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;
        var previousAssignment = (
            from pip in playerInPostion_table
            where pip.PlayerId == command.PlayerId && pip.Position == command.Position.GetPlayerPosition()
            select pip
        ).FirstOrDefault();

        if (previousAssignment == null)
            ThrowError("Player is not assigned to this position", StatusCodes.Status400BadRequest);
    }

}
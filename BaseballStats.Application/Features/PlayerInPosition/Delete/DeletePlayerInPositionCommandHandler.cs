using System.Security.Cryptography.X509Certificates;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BaseballStats.Application.Features.PlayerInPosition.Delete;

public class DeletePlayerInPositionCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<DeletePlayerInPositionCommand, PlayerInPositionCRUDDto>
{
    public override async Task<PlayerInPositionCRUDDto> ExecuteAsync(DeletePlayerInPositionCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var playerInPositionRepository = unitOfWork.Repository<Domain.Entities.PlayerInPosition>();
        var entity = await playerInPositionRepository.DeleteAsync(command.PlayerId, command.Position.GetPlayerPosition());

        await unitOfWork.SaveChangesAsync(ct);

        return new PlayerInPositionCRUDDto()
        {
            PlayerId = entity!.PlayerId,
            Position = entity.Position.GetDisplayName(),
            Effectiveness = entity.Effectiveness
        };

    }

    private async Task DatabaseValidations(DeletePlayerInPositionCommand command)
    {
        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;

        var entity = (
            from pip in playerInPosition_table
            where pip.PlayerId == command.PlayerId && pip.Position == command.Position.GetPlayerPosition()
            select pip
        ).FirstOrDefault();

        if (entity is null)
            ThrowError("Player not found", StatusCodes.Status404NotFound);

        await Task.CompletedTask;

    }
}
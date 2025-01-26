using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.PlayerInPosition.Get;

public class GetPlayerInPositionCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetPlayerInPositionCommand, PlayerInPositionCRUDDto>
{
    public override async Task<PlayerInPositionCRUDDto> ExecuteAsync(GetPlayerInPositionCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var playerInPositionRepository = unitOfWork.Repository<Domain.Entities.PlayerInPosition>();
        var pip = (await playerInPositionRepository.GetByIdAsync(command.PlayerId, command.Position.GetPlayerPosition()))!;

        return new PlayerInPositionCRUDDto()
        {
            PlayerId = pip.PlayerId,
            Position = pip.Position.GetDisplayName(),
            Effectiveness = pip.Effectiveness
        };
    }

    private async Task DatabaseValidations(GetPlayerInPositionCommand command)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = await playerRepository.GetByIdAsync(command.PlayerId);

        if (player is null)
            ThrowError("Player not found", StatusCodes.Status404NotFound);
    
        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;
        var entity = (
            from pip in playerInPosition_table
            where pip.PlayerId == command.PlayerId && pip.Position == command.Position.GetPlayerPosition()
            select pip
        ).FirstOrDefault();

        if (entity is null)
            ThrowError("Player is not assigned to specified position", StatusCodes.Status404NotFound);   

    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Domain.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using BaseballStats.Application.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BaseballStats.Application.Features.PlayerInPosition.Post;

public class PostPlayerInPositionCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostPlayerInPositionCommand, PlayerInPositionCRUDDto>
{
    public override async Task<PlayerInPositionCRUDDto> ExecuteAsync(PostPlayerInPositionCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidations(command);

        var playerInPositionRepository = unitOfWork.Repository<Domain.Entities.PlayerInPosition>();
        var createdPip = await playerInPositionRepository.AddAsync(
            new()
            {
                PlayerId = command.PlayerId,
                Position = command.Position.GetPlayerPosition(),
                Effectiveness = command.Effectiveness
            }
        );

        await unitOfWork.SaveChangesAsync(ct);

        return new()
            {
                PlayerId = createdPip.PlayerId,
                Position = createdPip.Position.GetDisplayName(),
                Effectiveness = createdPip.Effectiveness
            };
    }

    private async Task DatabaseValidations(PostPlayerInPositionCommand command)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = await playerRepository.GetByIdAsync(command.PlayerId);

        if (player is null)
            ThrowError("Player not found", StatusCodes.Status404NotFound);

        var playerInPostion_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;
        var previousAssignment =
            from pip in playerInPostion_table
            where pip.PlayerId == command.PlayerId && pip.Position == command.Position.GetPlayerPosition()
            select pip;

        if (previousAssignment.Any())
            ThrowError("Player is already assigned to this position", StatusCodes.Status400BadRequest);

    }
}
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Application.DTOs;
using FastEndpoints;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Pitcher.Put;

public class PutPitcherCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PutPitcherCommand, PitcherDto>
{
    public override async Task<PitcherDto> ExecuteAsync(PutPitcherCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var pitcherRepository = unitOfWork.Repository<Domain.Entities.Pitcher>();
        var player_table = unitOfWork.Repository<Domain.Entities.Player>().DbSet;

        var player = (
            from p in player_table
            where p.Id == command.PlayerId
            select p
        ).First();

        var entity = (await pitcherRepository.GetByIdAsync(command.PlayerId))!;

        entity.Player = player;
        entity.GamesWonNumber = command.GamesWonNumber;
        entity.GamesLostNumber = command.GamesLostNumber;
        entity.RightHanded = command.RightHanded;
        entity.AllowedRunsAvg = command.AllowedRunsAvg;

        var updPitcher = await pitcherRepository.UpdateAsync(entity);
        
        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;

        var entityPip = (
            from pip in playerInPosition_table
            where pip.PlayerId == command.PlayerId && pip.Position == Domain.Enums.PlayerPositions.Pitcher
            select pip
        ).Single();
        
        entityPip.Effectiveness = command.Effectiveness;
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return updPitcher.ToDto();
    }

    private async Task DatabaseValidations(PutPitcherCommand command)
    {
        var player_table = unitOfWork.Repository<Domain.Entities.Player>().DbSet;

        var player = (
            from p in player_table
            where p.Id == command.PlayerId
            select p
        ).ToList();

        if (player.IsNullOrEmpty())
            ThrowError("PlayerId not exists", StatusCodes.Status404NotFound);

        var pitcherRepo = unitOfWork.Repository<Domain.Entities.Pitcher>();
        var pitcher = await pitcherRepo.GetByIdAsync(command.PlayerId);

        if (pitcher is null)
            ThrowError("PlayerId is not a pitcher", StatusCodes.Status404NotFound);

        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;

        var entityPip = (
            from pip in playerInPosition_table
            where pip.PlayerId == command.PlayerId && pip.Position == Domain.Enums.PlayerPositions.Pitcher
            select pip
        ).ToList();

        if (entityPip.IsNullOrEmpty())
            ThrowError("PlayerId is not a pitcher", StatusCodes.Status404NotFound);

        await Task.CompletedTask;
    }
}
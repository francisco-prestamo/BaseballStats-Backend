using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Game.UpdateAlignment;

public class UpdateAlignmentCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<UpdateAlignmentCommand, SingleAlignmentDto>
{
    public override async Task<SingleAlignmentDto> ExecuteAsync(UpdateAlignmentCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidations(command);

        var alignedPlayerInGameRepository = unitOfWork.Repository<AlignedPlayerInGame>();

        alignedPlayerInGameRepository.DropWhere(x => x.GameId == command.GameId && x.TeamId == command.TeamId);

        var alignedPlayers = command.Alignment.Select(pip => new AlignedPlayerInGame
        {
            GameId = command.GameId,
            TeamId = command.TeamId,
            PlayerId = pip.Player.Id,
            Position = pip.Position.GetPlayerPosition()
        });

        alignedPlayerInGameRepository.AddRange(alignedPlayers);

        await unitOfWork.SaveChangesAsync(ct);

        return new SingleAlignmentDto
        {
            GameId = command.GameId,
            TeamId = command.TeamId,
            PlayersInPosition = command.Alignment
        };
    }

    private async Task DatabaseValidations(UpdateAlignmentCommand command)
    {
        
        var teamRepository = unitOfWork.Repository<Team>();
        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();

        var game = await gameRepository.GetByIdAsync(command.GameId);

        if (game is null)
            ThrowError("Game not found", StatusCodes.Status404NotFound);

        var team = await teamRepository.GetByIdAsync(command.TeamId);

        if (team is null)
            ThrowError("TeamId does not exist", StatusCodes.Status400BadRequest);

        
        var playerIds = 
            from pip in command.Alignment
            group pip by pip.Player.Id into g
            select g.Key;

        var playerInSeries = unitOfWork.Repository<PlayerInSeries>().DbSet;
        
        var playersInCorrectTeam =
            from pis in playerInSeries
            join p in playerIds on pis.PlayerId equals p
            where pis.TeamId == command.TeamId
            select pis.PlayerId;

        if (playerIds.Except(playersInCorrectTeam).Any())
        {
            ThrowError("Players must be assigned to provided team in the provided game's series", StatusCodes.Status400BadRequest);
        }

        var playerInPosition_table = unitOfWork.Repository<PlayerInPosition>().DbSet;

        var playersInCorrectPosition =
            from c_pip in command.Alignment
            join pip in playerInPosition_table on new { PlayerId = c_pip.Player.Id, c_pip.Position } equals new { pip.PlayerId, Position = pip.Position.GetDisplayName() }
            select c_pip.Player.Id;


        if (playerIds.Except(playersInCorrectPosition).Any()){
            ThrowError("Players must be able to play in the provided positions", StatusCodes.Status400BadRequest);
        }
    }
}
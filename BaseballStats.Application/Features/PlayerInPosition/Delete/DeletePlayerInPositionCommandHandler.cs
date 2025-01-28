using System.Security.Cryptography.X509Certificates;
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Application.Services;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Enums;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BaseballStats.Application.Features.PlayerInPosition.Delete;

public class DeletePlayerInPositionCommandHandler(IUnitOfWork unitOfWork, SubstitutionService substitutionService) : CommandHandler<DeletePlayerInPositionCommand, PlayerInPositionCRUDDto>
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
        var player = await unitOfWork.Repository<Domain.Entities.Player>().GetByIdAsync(command.PlayerId);

        if (player is null)
            ThrowError("Player not found", StatusCodes.Status404NotFound);

        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;

        var entity = (
            from pip in playerInPosition_table
            where pip.PlayerId == command.PlayerId && pip.Position == command.Position.GetPlayerPosition()
            select pip
        ).FirstOrDefault();

        if (entity is null)
            ThrowError("Player is not assigned to provided position", StatusCodes.Status404NotFound);

        ValidatePlayerDoesNotParticipateInPositionInAnyGames(command.PlayerId, command.Position.GetPlayerPosition());

        await Task.CompletedTask;
    }

    private void ValidatePlayerDoesNotParticipateInPositionInAnyGames(long playerId, PlayerPositions position)
    {
        var alignedPlayerInGame_table = unitOfWork.Repository<Domain.Entities.AlignedPlayerInGame>().DbSet;
        var gameId = (
            from apig in alignedPlayerInGame_table
            where apig.PlayerId == playerId && apig.Position == position
            select apig.GameId
        ).Cast<long?>().FirstOrDefault();

        if (gameId.HasValue)
            ThrowError($"Player is in the initial alignment of at least one game in the provided position (e.g. game with id {gameId.Value})", StatusCodes.Status400BadRequest);
    

        var substitutions_table = unitOfWork.Repository<Domain.Entities.Substitution>().DbSet;
        var games_table = unitOfWork.Repository<Domain.Entities.Game>().DbSet;

        var gamesWherePlayerParticipated = (
            from apig in alignedPlayerInGame_table
            where apig.PlayerId == playerId
            select apig.GameId
        ).Union(
            from s in substitutions_table
            join g in games_table on s.GameId equals g.Id
            where s.PlayerInId == playerId || s.PlayerOutId == playerId
            select g.Id
        );

        var playerInSeries_table = unitOfWork.Repository<Domain.Entities.PlayerInSeries>().DbSet;

        var gameInitialAlignments = (
            from apig in alignedPlayerInGame_table // get all initial alignments
            join g in games_table on apig.GameId equals g.Id // get game information
            // join with playerInSeries, to delete games in series where the player was not part of a team
            // and to get the teamId so we only get the initial alignmnents for the player's team in the game
            join pis in playerInSeries_table on new { g.SeriesId, PlayerId = playerId } equals new { pis.SeriesId, pis.PlayerId }
            where apig.TeamId == pis.TeamId && gamesWherePlayerParticipated.Contains(apig.GameId) // only get games where the player participated
            group apig by apig.GameId into gameGroup
            select new {
                gameId = gameGroup.Key,
                initialAlignment = gameGroup.Select(gameData => new {
                    gameData.PlayerId,
                    gameData.Position
                })
            }
        ).ToDictionary(x => x.gameId, x => x.initialAlignment);

        // this is similar as with the initial alignments, but we are looking for substitutions
        var gameSubstitutions = (
            from s in substitutions_table
            join g in games_table on s.GameId equals g.Id
            join pis in playerInSeries_table on new { g.SeriesId, PlayerId = playerId } equals new { pis.SeriesId, pis.PlayerId }
            where s.TeamId == pis.TeamId && gamesWherePlayerParticipated.Contains(s.GameId)
            group s by s.GameId into gameGroup
            select new {
                gameId = gameGroup.Key,
                substitutions = gameGroup.Select(substitutionData => new Substitution {
                    PlayerInId = substitutionData.PlayerInId,
                    PlayerOutId = substitutionData.PlayerOutId,
                    Time = substitutionData.Time,
                    GameId = substitutionData.GameId
                })
            }
        ).AsNoTracking().ToDictionary(x => x.gameId, x => x.substitutions);

        
        foreach (var id in gamesWherePlayerParticipated)
        {
            var substitutionsWithPositions = substitutionService.GetSubstitutionWithPositionsForATeamInAGame(
                gameInitialAlignments[id].Select(x => (x.PlayerId, x.Position)).ToList(),
                gameSubstitutions[id].AsQueryable() 
            );

            if (substitutionsWithPositions.Any(x => x.Position == position))
                ThrowError($"Player was substituted in the provided position in at least one game (e.g. game with id {id})", StatusCodes.Status400BadRequest);
        }
    }
}
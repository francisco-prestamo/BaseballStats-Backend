using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Enums;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

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
        
        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();
        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();
        var game_table = unitOfWork.Repository<Domain.Entities.Game>().DbSet;

        var game = await gameRepository.GetByIdAsync(command.GameId);

        if (game is null)
            ThrowError("Game not found", StatusCodes.Status404NotFound);

        var team = await teamRepository.GetByIdAsync(command.TeamId);

        if (team is null)
            ThrowError("TeamId does not exist", StatusCodes.Status400BadRequest);

        
        if (!ValidatePlayersAreInCorrectTeam(command))
        {
            ThrowError("Players must be assigned to provided team in the provided game's series", StatusCodes.Status400BadRequest);
        }

        var initialAlignment = (
            from a in command.Alignment
            select (a.Player.Id, a.Position.GetPlayerPosition())
        ).ToList();
        var allowedPositions = GetAllowedPositions(game.SeriesId, command.TeamId);
        System.Console.WriteLine($"AllowedPositions: {string.Join(", ", allowedPositions.Select(x => $"{x.Key}: {string.Join(", ", x.Value)}"))}");
        System.Console.WriteLine();
        var substitutions = GetSubstitutions(command.GameId, command.TeamId);
        
        switch (ValidateSubstitutions(initialAlignment, substitutions, allowedPositions))
        {
            case ValidateSubstitutionsErrorCode.PlayerCannotPlayInPosition:
                ThrowError("Players must be able to play in the provided positions, and positions they are substituted into", StatusCodes.Status400BadRequest);
                break;
            case ValidateSubstitutionsErrorCode.PlayerCannotEnterGameTwice:
                ThrowError("Players cannot enter the game twice", StatusCodes.Status400BadRequest);
                break;
            case ValidateSubstitutionsErrorCode.PlayerInBenchCannotLeaveGame:
                ThrowError("Players in the bench cannot be substituted out of a game", StatusCodes.Status400BadRequest);
                break;
        }
    }

    private bool ValidatePlayersAreInCorrectTeam(UpdateAlignmentCommand command)
    {
        var playerInSeries_table = unitOfWork.Repository<PlayerInSeries>().DbSet;

        var playerIds = 
            from pip in command.Alignment
            group pip by pip.Player.Id into g
            select g.Key;

        var playersInCorrectTeam =
            from pis in playerInSeries_table
            join p in playerIds on pis.PlayerId equals p
            where pis.TeamId == command.TeamId
            select pis.PlayerId;

        return !playerIds.Except(playersInCorrectTeam).Any();
    }

    /// <summary>
    /// Validates the ability of the players to play in the provided positions, 
    /// assumes initial alignment has single player per position and vice versa
    /// and that positions are valid
    /// </summary>
    /// <param name="initialAlignment">The initial alignment, assumed to have a single player per position and vice versa</param>
    /// <param name="substitutions">The substitutions during the game</param>
    /// <param name="availablePositions">The positions each player can play</param>
    private ValidateSubstitutionsErrorCode ValidateSubstitutions(List<(long PlayerId, PlayerPositions Position)> initialAlignment, List<Substitution> substitutions, Dictionary<long, HashSet<PlayerPositions>> availablePositions)
    {
        var playersInAllowedPosition = 
            from pip in initialAlignment
            join ap in availablePositions on pip.PlayerId equals ap.Key
            where ap.Value.Contains(pip.Position)
            select pip.PlayerId;
        
        if (initialAlignment.Select(x => x.PlayerId).Except(playersInAllowedPosition).Any())
            return ValidateSubstitutionsErrorCode.PlayerCannotPlayInPosition;

        substitutions = substitutions.OrderBy(x => x.Time).ToList();
        
        var usedPlayers = initialAlignment.Select(x => x.PlayerId).ToHashSet();
        var currentPosition = initialAlignment.ToDictionary(x => x.PlayerId, x => x.Position);

        bool isInGame(long playerId) => currentPosition.ContainsKey(playerId);
        bool hasPlayed(long playerId) => usedPlayers.Contains(playerId);
        bool canPlay(long playerId, PlayerPositions position) => availablePositions[playerId].Contains(position);

        foreach (var substitution in substitutions)
        {
            var playerIn = substitution.PlayerInId;
            var playerOut = substitution.PlayerOutId;

            System.Console.WriteLine();
            System.Console.WriteLine($"PlayerIn: {playerIn}, PlayerOut: {playerOut}");
            System.Console.WriteLine($"CurrentPosition: {string.Join(", ", currentPosition.Select(x => $"{x.Key}: {x.Value}"))}");
            System.Console.WriteLine($"UsedPlayers: {string.Join(", ", usedPlayers)}");


            // players that are in the bench cannot be substituted out of the game 
            if (!isInGame(playerOut))
                return ValidateSubstitutionsErrorCode.PlayerInBenchCannotLeaveGame;

            if (hasPlayed(playerIn))
            {
                // players that have entered the game cannot enter it again
                if (!isInGame(playerIn))
                    return ValidateSubstitutionsErrorCode.PlayerCannotEnterGameTwice;

                // if we reach this point, both players are in the game
                var playerInPosition = currentPosition[playerIn];
                var playerOutPosition = currentPosition[playerOut];

                if (!canPlay(playerIn, playerOutPosition) || !canPlay(playerOut, playerInPosition))
                    return ValidateSubstitutionsErrorCode.PlayerCannotPlayInPosition;

                currentPosition[playerIn] = playerOutPosition;
                currentPosition[playerOut] = playerInPosition;
            }
            else // playerIn was on the bench
            {
                if (!canPlay(playerIn, currentPosition[playerOut]))
                    return ValidateSubstitutionsErrorCode.PlayerCannotPlayInPosition;

                currentPosition[playerIn] = currentPosition[playerOut];
                currentPosition.Remove(playerOut);

                usedPlayers.Add(playerIn);
            }
        }

        return ValidateSubstitutionsErrorCode.OK;
    }

    private enum ValidateSubstitutionsErrorCode
    {
        PlayerCannotPlayInPosition,
        PlayerCannotEnterGameTwice,
        PlayerInBenchCannotLeaveGame,
        OK
    }

    private Dictionary<long, HashSet<PlayerPositions>> GetAllowedPositions(long seriesId, long teamId)
    {
        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;
        var playerInSeries_table = unitOfWork.Repository<PlayerInSeries>().DbSet;

        var allowedPositionsForTeamPlayers = (
            from pis in playerInSeries_table 
            join pip in playerInPosition_table on pis.PlayerId equals pip.PlayerId
            where pis.SeriesId == seriesId && pis.TeamId == teamId
            group pip by pis.PlayerId into g
            select new {PlayerId = g.Key, Position = g.Select(x => x.Position).ToHashSet()}
        ).ToList().Select(x => (x.PlayerId, x.Position)).ToDictionary(x => x.PlayerId, x => x.Position);

        System.Console.WriteLine($"AllowedPositionsForTeamPlayers: {string.Join(", ", allowedPositionsForTeamPlayers.Select(x => $"{x.Key}: {x.Value}"))}");

        return allowedPositionsForTeamPlayers;
    }

    private List<Substitution> GetSubstitutions(long gameId, long teamId)
    {
        var substitution_table = unitOfWork.Repository<Substitution>().DbSet;

        return (
            from s in substitution_table
            where s.GameId == gameId && s.TeamId == teamId
            select s
        ).ToList();
    }
}
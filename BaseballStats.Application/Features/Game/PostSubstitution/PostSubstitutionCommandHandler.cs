using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using BaseballStats.Application.Services;
using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.Features.Game.PostSubstitution;

public class PostSubstitutionCommandHandler(SubstitutionService substitutionService, IUnitOfWork unitOfWork) : CommandHandler<PostSubstitutionCommand, SingleSubstitutionCRUDDto>
{
    public override async Task<SingleSubstitutionCRUDDto> ExecuteAsync(PostSubstitutionCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var player_table = unitOfWork.Repository<Domain.Entities.Player>().DbSet;
        var game_table = unitOfWork.Repository<Domain.Entities.Game>().DbSet;
        var team_table = unitOfWork.Repository<Domain.Entities.Team>().DbSet;

        var playerIn = (
            from pi in player_table
            where pi.Id == command.PlayerInId
            select pi
        ).First();

        var playerOut = (
            from po in player_table
            where po.Id == command.PlayerOutId
            select po
        ).First();

        var game = (
            from g in game_table
            where g.Id == command.GameId
            select g
        ).First();

        var team = (
            from t in team_table
            where t.Id == command.TeamId
            select t
        ).First();

        var entity = new Domain.Entities.Substitution()
        {
            PlayerInId = command.PlayerInId,
            PlayerIn = playerIn,
            PlayerOutId = command.PlayerOutId,
            PlayerOut = playerOut,
            GameId = command.GameId,
            Game = game,
            TeamId = command.TeamId,
            Team = team,
            Time = command.Time
        };

        var repository = unitOfWork.Repository<Domain.Entities.Substitution>();

        var createdSubstitution = await repository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new SingleSubstitutionCRUDDto()
        {
            Id = command.GameId,
            TeamId = command.TeamId,
            PlayerInId = command.PlayerInId,
            PlayerOutId = command.PlayerOutId,
            Time = command.Time
        };
    }

    private async Task DatabaseValidations(PostSubstitutionCommand command)
    {
        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();
        var game = await gameRepository.GetByIdAsync(command.GameId);

        if (game is null)
            ThrowError("GameId not found", StatusCodes.Status404NotFound);

        var games = gameRepository.Where(x => x.Id == command.GameId).ToList().First();
        var team1Id = games.Team1Id;
        var team2Id = games.Team2Id;

        if (team1Id != command.TeamId && team2Id != command.TeamId)
            ThrowError("TeamId not found in the game", StatusCodes.Status404NotFound);

        var canAdd = substitutionService.CanAddSubstitution(command.GameId, command.TeamId, new Domain.Entities.Substitution
        {
            PlayerInId = command.PlayerInId,
            PlayerOutId = command.PlayerOutId,
            Time = command.Time
        });

        if (!canAdd.Result.status)
            ThrowError(canAdd.Result.errorMessage, StatusCodes.Status400BadRequest);
    }
}
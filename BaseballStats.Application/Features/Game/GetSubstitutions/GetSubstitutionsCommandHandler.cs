using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using BaseballStats.Application.Services;
using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.Features.Game.GetSubstitutions;

public class GetSubstitutionsCommandHandler(SubstitutionService substitutionService, IUnitOfWork unitOfWork) : CommandHandler<GetSubstitutionsCommand, GameSubstitutionsDto>
{
    public override async Task<GameSubstitutionsDto> ExecuteAsync(GetSubstitutionsCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var gameId = command.GameId;

        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();

        var games = gameRepository.Where(x => x.Id == gameId).ToList();
        var team1Id = games.First().Team1Id;
        var team2Id = games.First().Team2Id;

        var substitutionsDto = new GameSubstitutionsDto()
        {
            Team1Substitutions = GetTeamSubstitutions(gameId, team1Id),
            Team2Substitutions = GetTeamSubstitutions(gameId, team2Id)
        };

        return substitutionsDto;
    }

    private async Task DatabaseValidations(GetSubstitutionsCommand command)
    {
        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();
        var game = await gameRepository.GetByIdAsync(command.GameId);

        if (game is null)
            ThrowError("GameId not found", StatusCodes.Status404NotFound);
    }

    private List<SingleSubstitutionDto> GetTeamSubstitutions(long gameId, long teamId)
    {
        var substitutionsWithPosition = substitutionService.GetSubstitutionsWithExtrasAsync(gameId, teamId).Result.ToList();

        var result = 
            from swp in substitutionsWithPosition
            select new SingleSubstitutionDto()
            {
                TeamId = teamId,
                PlayerIn = GetPlayerInPositionDto(swp.PlayerInId, swp.Position),
                PlayerOut = GetPlayerInPositionDto(swp.PlayerOutId, swp.Position),
                Time = swp.Time
            };
        
        return result.ToList();
    }

    private PlayerInPositionDto GetPlayerInPositionDto(long playerId, PlayerPositions position)
    {
        var player_table = unitOfWork.Repository<Domain.Entities.Player>().DbSet;
        var playerInPosition_table = unitOfWork.Repository<Domain.Entities.PlayerInPosition>().DbSet;

        var result = 
            from p in player_table
            join pip in playerInPosition_table on p.Id equals pip.PlayerId
            where p.Id == playerId && pip.Position == position
            select new PlayerInPositionDto
            {
                Player = p.ToDto(),
                Position = pip.Position.GetDisplayName(),
                Effectiveness = pip.Effectiveness
            };
        
        return result.First();
    }
}

using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Application.Services;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Game.GetAlignmentFromTeam;

public class GetAlignmentFromTeamCommandHandler(AlignmentService alignmentService, IUnitOfWork unitOfWork) : CommandHandler<GetAlignmentFromTeamCommand, List<PlayerInPositionDto>>
{

    public override async Task<List<PlayerInPositionDto>> ExecuteAsync(GetAlignmentFromTeamCommand command, CancellationToken ct)
    {
        await DatabaseValidations(command, ct);

        var teamId = command.TeamId;
        var gameId = command.GameId;

        var players = await alignmentService.GetAlignmentsFromGame(gameId, teamId);

        return players.Select(x => x.GetPlayerInPositionDto()).ToList();
    }
    
    private async Task DatabaseValidations(GetAlignmentFromTeamCommand command, CancellationToken ct)
    {
        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();
        var game = await gameRepository.GetByIdAsync(command.GameId);

        if (game is null)
            ThrowError("GameId not found", StatusCodes.Status404NotFound);
        
        var gameId = command.GameId;

        var games = gameRepository.Where(x => x.Id == gameId).ToList();
        var team1Id = games.First().Team1Id;
        var team2Id = games.First().Team2Id;

        if (team1Id != command.TeamId && team2Id != command.TeamId)
            ThrowError("TeamId not found in the game", StatusCodes.Status404NotFound);
    }
}
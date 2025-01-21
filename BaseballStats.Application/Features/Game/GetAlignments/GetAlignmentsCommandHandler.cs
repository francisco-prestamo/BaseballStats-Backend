using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using BaseballStats.Application.Services;

namespace BaseballStats.Application.Features.Game.GetAlignments;

public class GetAlignmentsCommandHandler(AlignmentService alignmentsService, IUnitOfWork unitOfWork) : CommandHandler<GetAlignmentsCommand, AlignmentsDto>
{
    public override async Task<AlignmentsDto> ExecuteAsync(GetAlignmentsCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var gameId = command.GameId;

        var game = (await unitOfWork.Repository<Domain.Entities.Game>().GetByIdAsync(gameId))!;
        var team1Id = game.Team1Id;
        var team2Id = game.Team2Id;

        var alignment1 = await alignmentsService.GetAlignmentsFromGame(gameId, team1Id);
        var alignment2 = await alignmentsService.GetAlignmentsFromGame(gameId, team2Id);

        return (team1Id, alignment1, team2Id, alignment2).ToDto();
    }

    private async Task DatabaseValidations(GetAlignmentsCommand command)
    {
        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();
        var game = await gameRepository.GetByIdAsync(command.GameId);

        if (game is null)
            ThrowError("GameId not found", StatusCodes.Status404NotFound);
        
        var gameId = command.GameId;

        var games = gameRepository.Where(x => x.Id == gameId).ToList();
    }
}
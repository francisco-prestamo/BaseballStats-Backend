using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Game.GetGameObject;

public class GetGameObjectCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetGameObjectCommand, GameDto>
{
    public override async Task<GameDto> ExecuteAsync(GetGameObjectCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var gameId = command.GameId;

        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();
        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();

        var game = (await gameRepository.GetByIdAsync(gameId))!;
        // assuming the database is consistent
        var team1 = teamRepository.Where(x => x.Id == game.Team1Id).Select(x => x.ToDto()).FirstOrDefault()!;
        var team2 = teamRepository.Where(x => x.Id == game.Team2Id).Select(x => x.ToDto()).FirstOrDefault()!;

        return game.ToDto(team1, team2);
    }

    private async Task DatabaseValidations(GetGameObjectCommand command)
    {
        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();
        var game = await gameRepository.GetByIdAsync(command.GameId);

        if (game is null)
            ThrowError("GameId not found", StatusCodes.Status404NotFound);
    }
}
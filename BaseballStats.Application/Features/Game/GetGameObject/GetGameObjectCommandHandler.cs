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

        var games = gameRepository.Where(x => x.Id == gameId).ToList();

        var gamesDto = games.Select(x => x.ToDto());

        return gamesDto.First();
    }

    private async Task DatabaseValidations(GetGameObjectCommand command)
    {
        var gameRepository = unitOfWork.Repository<Domain.Entities.Game>();
        var game = await gameRepository.GetByIdAsync(command.GameId);

        if (game is null)
            ThrowError("GameId not found", StatusCodes.Status404NotFound);
    }
}
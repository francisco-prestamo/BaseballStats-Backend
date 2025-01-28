using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Player.Delete;

public class DeletePlayerCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<DeletePlayerCommand, EmptyResponse>
{
    public override async Task<EmptyResponse> ExecuteAsync(DeletePlayerCommand command, CancellationToken cancellationToken)
    {
        await DatabaseValidations(command);
        
        var id = command.PlayerId;

        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();

        await playerRepository.DeleteAsync(id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new EmptyResponse();
    }

    private async Task DatabaseValidations(DeletePlayerCommand command)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = await playerRepository.GetByIdAsync(command.PlayerId);

        if (player is null)
            ThrowError("PlayerId not found", StatusCodes.Status404NotFound);

        var alignedPlayerInGame_table = unitOfWork.Repository<Domain.Entities.AlignedPlayerInGame>().DbSet;
        var gameId = (
            from apig in alignedPlayerInGame_table
            where apig.PlayerId == command.PlayerId
            select apig.GameId
        ).Cast<long?>().FirstOrDefault();

        if (gameId.HasValue)
            ThrowError($"Player participates in the initial alignment of at least one game (e.g. game with id {gameId})", StatusCodes.Status400BadRequest);
        
        var substitution_table = unitOfWork.Repository<Domain.Entities.Substitution>().DbSet;
        gameId = (
            from s in substitution_table
            where s.PlayerInId == command.PlayerId || s.PlayerOutId == command.PlayerId
            select s.GameId
        ).Cast<long?>().FirstOrDefault();

        if (gameId.HasValue)
            ThrowError($"Player participates in a substitution of at least one game (e.g. game with id {gameId})", StatusCodes.Status400BadRequest);
    }
}
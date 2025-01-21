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
    }
}
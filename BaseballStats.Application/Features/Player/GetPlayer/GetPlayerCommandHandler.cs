using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Interfaces.DataAccess;
using Microsoft.AspNetCore.Http;
using BaseballStats.Application.Mappers;

using FastEndpoints;

namespace BaseballStats.Application.Features.Player.GetPlayer;

public class GetPlayerCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetPlayerCommand, RegularPlayerDto>
{
    public  override async Task<RegularPlayerDto> ExecuteAsync(GetPlayerCommand command, CancellationToken cancellationToken)
    {
        await DatabaseValidations(command);

        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = (await playerRepository.GetByIdAsync(command.Id))!;

        return player.ToDto();        
    }

    private async Task DatabaseValidations(GetPlayerCommand command)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = await playerRepository.GetByIdAsync(command.Id);

        if (player is null)
            ThrowError("PlayerId not found", StatusCodes.Status404NotFound);
    }
}
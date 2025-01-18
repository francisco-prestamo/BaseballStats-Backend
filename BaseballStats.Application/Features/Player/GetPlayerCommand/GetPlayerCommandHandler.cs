using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Player.GetPlayer;

public class GetPlayerCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetPlayerCommand, RegularPlayerDto>
{
    public override async Task<RegularPlayerDto> ExecuteAsync(GetPlayerCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();

        var entity = (await playerRepository.GetByIdAsync(command.Id))!;

        return entity.ToDto();
    }

    private async Task DatabaseValidations(GetPlayerCommand command)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = await playerRepository.GetByIdAsync(command.Id);

        if (player is null)
            ThrowError("PlayerId not found", StatusCodes.Status404NotFound);
    }
}
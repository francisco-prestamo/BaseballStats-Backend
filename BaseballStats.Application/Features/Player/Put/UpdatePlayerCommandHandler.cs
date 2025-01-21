using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Application.DTOs;
using FastEndpoints;
using BaseballStats.Application.Features.Player.Delete;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Player.Put;

public class UpdatePlayerCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<UpdatePlayerCommand, RegularPlayerDto>
{
    public override async Task<RegularPlayerDto> ExecuteAsync(UpdatePlayerCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();

        var entity = (await playerRepository.GetByIdAsync(command.Id))!;
        
        entity.Name = command.Name;
        entity.Age = command.Age;
        entity.YearsOfExperience = command.YearsOfExperience;
        entity.BattingAverage = command.BattingAverage;

        var updatedPlayer = await playerRepository.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return updatedPlayer.ToDto();
    }

    private async Task DatabaseValidations(UpdatePlayerCommand command)
    {
        var playerRepository = unitOfWork.Repository<Domain.Entities.Player>();
        var player = await playerRepository.GetByIdAsync(command.Id);

        if (player is null)
            ThrowError("PlayerId not found", StatusCodes.Status404NotFound);
    }
}
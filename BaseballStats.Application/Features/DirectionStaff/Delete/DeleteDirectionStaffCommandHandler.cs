using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using BaseballStats.Application.Mappers;

namespace BaseballStats.Application.Features.DirectionStaff.Delete;

public class DeleteDirectionStaffCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<DeleteDirectionStaffCommand, DirectionStaffDto>
{
    public override async Task<DirectionStaffDto> ExecuteAsync(DeleteDirectionStaffCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var directionStaff = await unitOfWork.Repository<Domain.Entities.DirectionStaff>().DeleteAsync(command.Id);
        
        await unitOfWork.SaveChangesAsync(ct);

        return directionStaff!.ToDto();
    }

    private async Task DatabaseValidations(DeleteDirectionStaffCommand command)
    {
        var directionStaff = await unitOfWork.Repository<Domain.Entities.DirectionStaff>().GetByIdAsync(command.Id);

        if (directionStaff is null)
            ThrowError("DirectionStaff not found", StatusCodes.Status404NotFound);
    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using BaseballStats.Application.Mappers;

namespace BaseballStats.Application.Features.DirectionStaff.Put;

public class PutDirectionStaffCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PutDirectionStaffCommand, DirectionStaffDto>
{
    public override async Task<DirectionStaffDto> ExecuteAsync(PutDirectionStaffCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var directionStaffRepository = unitOfWork.Repository<Domain.Entities.DirectionStaff>();
        
        var directionStaff = await directionStaffRepository.GetByIdAsync(command.Id);

        directionStaff!.Name = command.Name;
        
        await unitOfWork.SaveChangesAsync(ct);

        return directionStaff!.ToDto();
    }

    private async Task DatabaseValidations(PutDirectionStaffCommand command)
    {
        var directionStaffRepository = unitOfWork.Repository<Domain.Entities.DirectionStaff>();
        
        var directionStaff = await directionStaffRepository.GetByIdAsync(command.Id);

        if (directionStaff is null)
            ThrowError("DirectionStaff not found", StatusCodes.Status404NotFound);
    }
}
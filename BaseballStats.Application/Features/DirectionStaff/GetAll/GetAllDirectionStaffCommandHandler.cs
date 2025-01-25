using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Interfaces.DataAccess;
using Microsoft.AspNetCore.Http;
using BaseballStats.Application.Mappers;

using FastEndpoints;

namespace BaseballStats.Application.Features.DirectionStaff.GetAll;

public class GetAllDirectionStaffCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetAllDirectionStaffCommand, List<DirectionStaffDto>>
{
    public override async Task<List<DirectionStaffDto>> ExecuteAsync(GetAllDirectionStaffCommand command, CancellationToken cancellationToken)
    {
        var directionStaffRepository = unitOfWork.Repository<Domain.Entities.DirectionStaff>();

        var entities = await directionStaffRepository.GetAllAsync();

        return entities.Select(x => x.ToDto()).ToList();
    }
}
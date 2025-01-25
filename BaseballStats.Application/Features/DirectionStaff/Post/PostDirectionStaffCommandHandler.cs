using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using BaseballStats.Application.Mappers;

namespace BaseballStats.Application.Features.DirectionStaff.Post;

public class PostDirectionStaffCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostDirectionStaffCommand, DirectionStaffDto>
{
    public override async Task<DirectionStaffDto> ExecuteAsync(PostDirectionStaffCommand command, CancellationToken ct = default)
    {
        var directionStaffRepository = unitOfWork.Repository<Domain.Entities.DirectionStaff>();

        var entity = new Domain.Entities.DirectionStaff()
        {
            Name = command.Name
        };

        var createdDirectionStaff = await directionStaffRepository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync(ct);

        return createdDirectionStaff.ToDto();
    }

}
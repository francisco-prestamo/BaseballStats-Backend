using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using BaseballStats.Application.Mappers;

namespace BaseballStats.Application.Features.User.GetAllUsers;

public class GetAllUsersCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetAllUsersCommand, List<RegisteredUserDto>>
{
    public override async Task<List<RegisteredUserDto>> ExecuteAsync(GetAllUsersCommand command, CancellationToken cancellationToken)
    {
        var userRepository = unitOfWork.Repository<Domain.Entities.Identity.RegisteredUser>();
        var users = await userRepository.GetAllAsync();

        return users.Select(x => x.ToDto()).ToList();
    }

}
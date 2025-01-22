using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Entities.Identity;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.User.Put;

public class UpdateUserCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<UpdateUserCommand, RegisteredUserDto>
{
    public override async Task<RegisteredUserDto> ExecuteAsync(UpdateUserCommand command, CancellationToken ct)
    {
        await DatabaseValidations(command);

        var userRepository = unitOfWork.Repository<Domain.Entities.Identity.RegisteredUser>();
        var user = (await userRepository.GetByIdAsync(command.Id))!;

        user.Password = command.Password ?? user.Password;
        user.Username = command.Username;
        user.Type = command.UserType.ToUserType();

        user = await userRepository.UpdateAsync(user);
        await unitOfWork.SaveChangesAsync(ct);

        return user.ToDto();
    }

    private async Task DatabaseValidations(UpdateUserCommand command)
    {   
        var userRepository = unitOfWork.Repository<Domain.Entities.Identity.RegisteredUser>();
        var user = await userRepository.GetByIdAsync(command.Id);

        if (user is null)
            ThrowError("UserId not found", StatusCodes.Status404NotFound);
    }

}
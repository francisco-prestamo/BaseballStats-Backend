using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Entities.Identity;
using BaseballStats.Domain.Enums;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Auth;

public class RegisterUserCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<RegisterUserCommand, RegisteredUserDto>
{
    public override async Task<RegisteredUserDto> ExecuteAsync(RegisterUserCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidations(command);

        var userRepository = unitOfWork.Repository<RegisteredUser>();

        RegisteredUser user;
        if (command.UserType.ToUserType() == UserTypes.TechnicalDirector)
        {
            user = new TechnicalDirector
            {
                Username = command.Username,
                Password = command.Password,
            };

            user = await userRepository.AddAsync(user);
            var technicalDirectorRepository = unitOfWork.Repository<TechnicalDirector>();
            await technicalDirectorRepository.AddAsync((TechnicalDirector) user);
            await unitOfWork.SaveChangesAsync(ct);

            return user.ToDto();

        }
        else
        {            
            user = new RegisteredUser
            {
                Username = command.Username,
                Password = command.Password,
                Type = command.UserType.ToUserType()
            };

            user = await userRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync(ct);

            return user.ToDto();
        }
        
    }

    private async Task DatabaseValidations(RegisterUserCommand command)
    {
        var userRepository = unitOfWork.Repository<RegisteredUser>();

        var user = await userRepository.FirstOrDefaultAsync(x => x.Username == command.Username);

        if (user is not null)
            ThrowError("Username already exists", StatusCodes.Status400BadRequest);
    }
}
using BaseballStats.Domain.Entities.Identity;
using BaseballStats.Domain.Enums;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Auth;

public class RegisterUserCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<RegisterUserCommand, RegisterUserResponse>
{
    public override async Task<RegisterUserResponse> ExecuteAsync(RegisterUserCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidations(command);

        var userRepository = unitOfWork.Repository<RegisteredUser>();

        RegisteredUser user;
        if (command.Role == UserTypes.TechnicalDirector.ToString())
        {
            user = new TechnicalDirector
            {
                Username = command.Username,
                Password = command.Password,
            };
        }
        else
        {
            if (! Enum.TryParse<UserTypes>(command.Role, out var role) )
                ThrowError("Invalid role, roles are 'Admin', 'TechnicalDirector' and 'Journalist'", StatusCodes.Status400BadRequest);
            
            user = new RegisteredUser
            {
                Username = command.Username,
                Password = command.Password,
                Type = role
            };
        }
        
        await userRepository.AddAsync(user);
        await unitOfWork.SaveChangesAsync(ct);
        
        return new RegisterUserResponse(user.Id, user.Username, user.Type.ToString());
    }

    private async Task DatabaseValidations(RegisterUserCommand command)
    {
        var userRepository = unitOfWork.Repository<RegisteredUser>();

        var user = await userRepository.FirstOrDefaultAsync(x => x.Username == command.Username);

        if (user is not null)
            ThrowError("Username already exists", StatusCodes.Status400BadRequest);
    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Entities.Identity;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BaseballStats.Application.Features.Auth;

public class LoginCommandHandler(IUnitOfWork unitOfWork, IConfiguration config) : CommandHandler<LoginCommand, RegisteredUserDto>
{
    public override async Task<RegisteredUserDto> ExecuteAsync(LoginCommand command, CancellationToken ct = new CancellationToken())
    {
        await ValidateUser(command);

        var userRepository = unitOfWork.Repository<RegisteredUser>();

        var user = await userRepository.FirstOrDefaultAsync(x => x.Username == command.Username);

        var token = JwtBearer.CreateToken(x =>
        {
            x.SigningKey = config["Jwt:SigningKey"]!;
            x.ExpireAt = DateTime.UtcNow.AddDays(1);
            x.User.Roles.Add(user!.Type.ToString());
            x.User.Claims.Add(("UserId", user.Id.ToString()));
        });

        return user!.ToDto();
    }

    private async Task ValidateUser(LoginCommand command)
    {
        var userRepository = unitOfWork.Repository<RegisteredUser>();

        var user = await userRepository.FirstOrDefaultAsync(x => x.Username == command.Username);

        if (user is null)
            ThrowError("Username is incorrect", StatusCodes.Status400BadRequest);

        if (user.Password != command.Password)
            ThrowError("Password is incorrect", StatusCodes.Status400BadRequest);
    }
}
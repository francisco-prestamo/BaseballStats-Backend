using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.User.Delete;

public class DeleteUserCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<DeleteUserCommand, RegisteredUserDto>
{
    public override async Task<RegisteredUserDto> ExecuteAsync(DeleteUserCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var userRepository = unitOfWork.Repository<Domain.Entities.Identity.RegisteredUser>();
        var user = (await userRepository.GetByIdAsync(command.Id))!;

        await userRepository.DeleteAsync(command.Id);
        await unitOfWork.SaveChangesAsync(ct);

        return user.ToDto();
    }

    private async Task DatabaseValidations(DeleteUserCommand command)
    {
        var userRepository = unitOfWork.Repository<Domain.Entities.Identity.RegisteredUser>();
        var user = await userRepository.GetByIdAsync(command.Id);

        if (user is null)
            ThrowError("UserId not found", StatusCodes.Status404NotFound);
    
        if (user.Type == Domain.Enums.UserTypes.TechnicalDirector)
        {
            var team_table = unitOfWork.Repository<Domain.Entities.Team>().DbSet;
            var directedTeam = (
                from t in team_table
                where t.TechnicalDirectorId == command.Id
                select t
            ).FirstOrDefault();

            if (directedTeam != null)
                ThrowError($"Cannot delete a Technical Director that is directing a team (e.g. directed team {directedTeam.Id})", StatusCodes.Status400BadRequest);
        }
    }
}
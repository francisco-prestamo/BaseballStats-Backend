using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BaseballStats.Application.Features.DirectionStaffTeamNamespace.Delete;

public class DeleteDirectionStaffTeamCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<DeleteDirectionStaffTeamCommand, DirectionStaffTeamDto>
{
    public override async Task<DirectionStaffTeamDto> ExecuteAsync(DeleteDirectionStaffTeamCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidations(command);

        var directionStaffTeamRepo = unitOfWork.Repository<Domain.Entities.DirectionStaffTeam>();
        
        var directionStaffTeam = directionStaffTeamRepo.DbSet;
        var directionStaff = unitOfWork.Repository<Domain.Entities.DirectionStaff>().DbSet;
        var team = unitOfWork.Repository<Domain.Entities.Team>().DbSet;

        directionStaffTeamRepo.DropWhere(x => x.TeamId == command.TeamId && x.DirectionStaffId == command.DirectionMemberId);

        await unitOfWork.SaveChangesAsync(ct);
        return new DirectionStaffTeamDto { DirectionMemberId = command.DirectionMemberId, TeamId = command.TeamId };
    }

    private async Task DatabaseValidations(DeleteDirectionStaffTeamCommand command)
    {
        var directionStaffTeam = unitOfWork.Repository<Domain.Entities.DirectionStaffTeam>().DbSet;

        var dst = (
            from st in directionStaffTeam
            where st.DirectionStaffId == command.DirectionMemberId && st.TeamId == command.TeamId
            select st
        ).ToList();

        if (dst.IsNullOrEmpty())
            ThrowError("Relation between TeamId and DirectionMemberId not exists", StatusCodes.Status400BadRequest);

        await Task.CompletedTask;
    }
}
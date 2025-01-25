using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.DirectionStaffTeam.Delete;

public class DeleteDirectionStaffTeamCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<DeleteDirectionStaffTeamCommand, DirectionStaffTeamDto>
{
    public override async Task<DirectionStaffTeamDto> ExecuteAsync(DeleteDirectionStaffTeamCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidations(command);

        var directionStaffRepo = unitOfWork.Repository<Domain.Entities.DirectionStaff>();
        var directionStaff = await directionStaffRepo.GetByIdAsync(command.DirectionMemberId);
        
        var teamRepo = unitOfWork.Repository<Domain.Entities.Team>();
        var team = await teamRepo.GetByIdAsync(command.TeamId);

        team!.DirectionStaffs.Remove(directionStaff!);
        directionStaff!.TeamsLead.Remove(team!);

        await directionStaffRepo.UpdateAsync(directionStaff);
        await teamRepo.UpdateAsync(team);
        
        await unitOfWork.SaveChangesAsync(ct);
        return new DirectionStaffTeamDto { DirectionMemberId = command.DirectionMemberId, TeamId = command.TeamId };
    }

    private async Task DatabaseValidations(DeleteDirectionStaffTeamCommand command)
    {
        var team = await unitOfWork.Repository<Team>().GetByIdAsync(command.TeamId);

        if (team is null)
            ThrowError("TeamId does not exist", StatusCodes.Status404NotFound);

        var directionStaff = await unitOfWork.Repository<Domain.Entities.DirectionStaff>().GetByIdAsync(command.DirectionMemberId);

        if (directionStaff is null)
            ThrowError("DirectionStaff does not exist", StatusCodes.Status404NotFound);

        bool found = false;
        foreach (var member in team.DirectionStaffs)
        {
            if (member.Id == command.DirectionMemberId)
                found = true;
        }

        if (!found)
            ThrowError("Relation between TeamId and DirectionMemberId does not exists", StatusCodes.Status400BadRequest);
    }
}
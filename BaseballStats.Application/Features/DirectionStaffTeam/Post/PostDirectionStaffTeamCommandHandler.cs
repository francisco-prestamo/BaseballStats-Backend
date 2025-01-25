using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.DirectionStaffTeam.Post;

public class PostDirectionStaffTeamCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostDirectionStaffTeamCommand, DirectionStaffTeamDto>
{
    public override async Task<DirectionStaffTeamDto> ExecuteAsync(PostDirectionStaffTeamCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidations(command);

        var directionStaffRepo = unitOfWork.Repository<Domain.Entities.DirectionStaff>();
        var directionStaff = await directionStaffRepo.GetByIdAsync(command.DirectionMemberId);
        
        var teamRepo = unitOfWork.Repository<Domain.Entities.Team>();
        var team = await teamRepo.GetByIdAsync(command.TeamId);
        
        team!.DirectionStaffs.Add(directionStaff!);
        directionStaff!.TeamsLead.Add(team!);

        await directionStaffRepo.UpdateAsync(directionStaff);
        await teamRepo.UpdateAsync(team);

        await unitOfWork.SaveChangesAsync(ct);
        return new DirectionStaffTeamDto { DirectionMemberId = command.DirectionMemberId, TeamId = command.TeamId };
    }

    private async Task DatabaseValidations(PostDirectionStaffTeamCommand command)
    {
        var team = await unitOfWork.Repository<Team>().GetByIdAsync(command.TeamId);

        if (team is null)
            ThrowError("TeamId does not exist", StatusCodes.Status404NotFound);

        var directionStaff = await unitOfWork.Repository<Domain.Entities.DirectionStaff>().GetByIdAsync(command.DirectionMemberId);

        if (directionStaff is null)
            ThrowError("DirectionStaff does not exist", StatusCodes.Status404NotFound);

        foreach (var member in team.DirectionStaffs)
        {
            if (member.Id == command.DirectionMemberId)
                ThrowError("Relation between TeamId and DirectionMemberId already exists", StatusCodes.Status400BadRequest);
        }
    }
}
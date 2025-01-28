using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BaseballStats.Application.Features.DirectionStaffTeamNamespace.Post;

public class PostDirectionStaffTeamCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostDirectionStaffTeamCommand, DirectionStaffTeamDto>
{
    public override async Task<DirectionStaffTeamDto> ExecuteAsync(PostDirectionStaffTeamCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidations(command);

        var directionStaffTeam = unitOfWork.Repository<Domain.Entities.DirectionStaffTeam>().DbSet;
        var directionStaff = unitOfWork.Repository<Domain.Entities.DirectionStaff>().DbSet;
        var team = unitOfWork.Repository<Domain.Entities.Team>().DbSet;

        var directionStaffInfo = (
            from ds in directionStaff
            where ds.Id == command.DirectionMemberId
            select ds
        ).First();

        var teamInfo = (
            from t in team
            where t.Id == command.TeamId
            select t
        ).First();

        var entity = new DirectionStaffTeam()
        {
            DirectionStaffId = directionStaffInfo.Id,
            DirectionStaff = directionStaffInfo,
            TeamId = teamInfo.Id,
            Team = teamInfo
        };

        var createdRelation = await directionStaffTeam.AddAsync(entity);

        await unitOfWork.SaveChangesAsync(ct);
        return new DirectionStaffTeamDto { DirectionMemberId = command.DirectionMemberId, TeamId = command.TeamId };
    }

    private async Task DatabaseValidations(PostDirectionStaffTeamCommand command)
    {
        var directionStaffTeam = unitOfWork.Repository<Domain.Entities.DirectionStaffTeam>().DbSet;

        var dst = (
            from st in directionStaffTeam
            where st.DirectionStaffId == command.DirectionMemberId && st.TeamId == command.TeamId
            select st
        ).ToList();

        if (!dst.IsNullOrEmpty())
            ThrowError("Relation between TeamId and DirectionMemberId already exists", StatusCodes.Status400BadRequest);

        await Task.CompletedTask;
    }
}
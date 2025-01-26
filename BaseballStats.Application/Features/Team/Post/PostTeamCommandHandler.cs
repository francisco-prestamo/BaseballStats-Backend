using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Entities.Identity;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Team.Post;

public class PostTeamCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostTeamCommand, TeamAdminDto>
{
    public override async Task<TeamAdminDto> ExecuteAsync(PostTeamCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidationAsync(command);

        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();

        var team = new Domain.Entities.Team
        {
            Name = command.Name,
            Initials = command.Initials,
            RepresentedEntity = command.RepresentedEntity,
            Color = command.Color,
            TechnicalDirectorId = command.DtId
        };

        await teamRepository.AddAsync(team);

        await unitOfWork.SaveChangesAsync(ct);

        return team.ToTeamAdminDto();
    }

    private async Task DatabaseValidationAsync(PostTeamCommand command)
    {
        var technicalDirector = await unitOfWork.Repository<TechnicalDirector>().GetByIdAsync(command.DtId);

        if (technicalDirector is null)
            ThrowError("TechnicalDirector not found", StatusCodes.Status400BadRequest);
    }
}
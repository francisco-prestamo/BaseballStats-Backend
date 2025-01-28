using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Entities.Identity;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Team.Put;

public class PutTeamCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PutTeamCommand, TeamAdminDto>
{
    public override async Task<TeamAdminDto> ExecuteAsync(PutTeamCommand command, CancellationToken ct = new CancellationToken())
    {
        await DatabaseValidationAsync(command);

        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();

        var team = await teamRepository.GetByIdAsync(command.Id);

        team!.Name = command.Name;
        team.Initials = command.Initials;
        team.RepresentedEntity = command.RepresentedEntity;
        team.Color = command.Color;
        team.TechnicalDirectorId = command.DtId;

        await unitOfWork.SaveChangesAsync(ct);

        return team.ToTeamAdminDto();
    }

    private async Task DatabaseValidationAsync(PutTeamCommand command)
    {
        var team = await unitOfWork.Repository<Domain.Entities.Team>().GetByIdAsync(command.Id);

        if (team is null)
            ThrowError("Team not found", StatusCodes.Status404NotFound);

        var technicalDirector = await unitOfWork.Repository<TechnicalDirector>().GetByIdAsync(command.DtId);

        if (technicalDirector is null)
            ThrowError("TechnicalDirector not found", StatusCodes.Status400BadRequest);
    }
}
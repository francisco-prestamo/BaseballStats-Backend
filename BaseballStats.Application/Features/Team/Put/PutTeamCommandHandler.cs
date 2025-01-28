using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Team.Put;

public class PutTeamCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PutTeamCommand, TeamDto>
{
    public override async Task<TeamDto> ExecuteAsync(PutTeamCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();

        var team = (await teamRepository.GetByIdAsync(command.Id))!;
        team.Name = command.Name;
        team.RepresentedEntity = command.RepresentedEntity;
        team.Initials = command.Initials;
        team.Color = command.Color;
        team.TechnicalDirectorId = command.DtId;


        await unitOfWork.SaveChangesAsync(ct);

        return team.ToDto();
    }

    private async Task DatabaseValidations(PutTeamCommand command)
    {
        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();
        var team = await teamRepository.GetByIdAsync(command.Id);

        if (team is null)
            ThrowError("Team not found", StatusCodes.Status404NotFound);

        var technicalDirectorRepository = unitOfWork.Repository<Domain.Entities.Identity.TechnicalDirector>();
        var technicalDirector = await technicalDirectorRepository.GetByIdAsync(command.DtId);

        if (technicalDirector is null)
            ThrowError("Technical Director not found", StatusCodes.Status404NotFound);
    }
}
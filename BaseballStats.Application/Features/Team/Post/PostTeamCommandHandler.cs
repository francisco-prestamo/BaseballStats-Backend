using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Team.Post;

public class PostTeamCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<PostTeamCommand, TeamDto>
{
    public override async Task<TeamDto> ExecuteAsync(PostTeamCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();

        var team = new Domain.Entities.Team()
        {
            Name = command.Name,
            RepresentedEntity = command.RepresentedEntity,
            Initials = command.Initials,
            Color = command.Color,
            TechnicalDirectorId = command.DtId
        };

        await teamRepository.AddAsync(team);

        await unitOfWork.SaveChangesAsync(ct);

        return team.ToDto();
    }

    private async Task DatabaseValidations(PostTeamCommand command)
    {
        var technicalDirectorRepository = unitOfWork.Repository<Domain.Entities.Identity.TechnicalDirector>();
        var technicalDirector = await technicalDirectorRepository.GetByIdAsync(command.DtId);

        if (technicalDirector is null)
            ThrowError("Technical Director not found", StatusCodes.Status404NotFound);
    
    }
}
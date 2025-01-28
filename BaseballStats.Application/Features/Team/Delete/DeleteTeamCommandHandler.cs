using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace BaseballStats.Application.Features.Team.Delete;

public class DeleteTeamCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<DeleteTeamCommand, TeamDto>
{
    public override async Task<TeamDto> ExecuteAsync(DeleteTeamCommand command, CancellationToken ct = default)
    {
        await DatabaseValidations(command);

        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();
        var team = (await teamRepository.DeleteAsync(command.Id))!;

        await unitOfWork.SaveChangesAsync(ct);

        return team.ToDto();
    }

    private async Task DatabaseValidations(DeleteTeamCommand command)
    {
        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();
        var team = await teamRepository.GetByIdAsync(command.Id);

        if (team is null)
            ThrowError("Team not found", StatusCodes.Status404NotFound);
    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BaseballStats.Application.Features.Team.Get;

public class GetTeamCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetTeamCommand, TeamDto>
{
    public override async Task<TeamDto> ExecuteAsync(GetTeamCommand command, CancellationToken cancellationToken = default)
    {
        await DatabaseValidations(command);

        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();
        var team = await teamRepository.GetByIdAsync(command.TeamId);
        return team!.ToDto();
    }

    private async Task DatabaseValidations(GetTeamCommand command)
    {
        var teamRepository = unitOfWork.Repository<Domain.Entities.Team>();
        var team = await teamRepository.GetByIdAsync(command.TeamId);

        if (team is null)
            ThrowError("TeamId not found", StatusCodes.Status404NotFound);
    }
}
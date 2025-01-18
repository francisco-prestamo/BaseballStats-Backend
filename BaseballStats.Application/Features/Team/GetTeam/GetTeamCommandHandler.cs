using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BaseballStats.Application.Features.Team.GetTeam;

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
        
        var seasonRepository = unitOfWork.Repository<Domain.Entities.Season>();
        var season = await seasonRepository.GetByIdAsync(command.SeasonId);

        if (season is null)
            ThrowError("SeasonId not found", StatusCodes.Status404NotFound);

        var seriesRepository = unitOfWork.Repository<Domain.Entities.Series>();
        var series = await seriesRepository.GetByIdAsync(command.SeriesId);

        if (series is null)
            ThrowError("SeriesId not found", StatusCodes.Status404NotFound);
    }
}
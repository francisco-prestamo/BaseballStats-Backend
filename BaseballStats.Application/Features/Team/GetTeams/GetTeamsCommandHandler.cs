using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.Team.GetTeams;

public class GetTeamsCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetTeamsCommand, List<TeamWithDtIdDto>>
{
    public override async Task<List<TeamWithDtIdDto>> ExecuteAsync(GetTeamsCommand command, CancellationToken ct = new CancellationToken())
    {
        var teamsRepository = unitOfWork.Repository<Domain.Entities.Team>();

        var teams = await teamsRepository.GetAllAsync();

        return teams.Select(x => x.ToTeamWithDtIdDto()).ToList();
    }
}
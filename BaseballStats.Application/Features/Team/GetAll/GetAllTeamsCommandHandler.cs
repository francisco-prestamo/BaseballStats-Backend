using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;

namespace BaseballStats.Application.Features.Team.GetAll;

public class GetAllTeamsCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetAllTeamsCommand, List<TeamDto>>
{
    public override async Task<List<TeamDto>> ExecuteAsync(GetAllTeamsCommand command, CancellationToken ct = default)
    {
        var repository = unitOfWork.Repository<Domain.Entities.Team>();

        var teams = (await repository.GetAllAsync()).Select(t => t.ToDto()).ToList();

        return teams;
    }

}
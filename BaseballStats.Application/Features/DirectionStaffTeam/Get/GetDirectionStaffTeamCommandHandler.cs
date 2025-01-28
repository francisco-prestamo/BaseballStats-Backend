using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using BaseballStats.Application.Services;

namespace BaseballStats.Application.Features.DirectionStaffTeamNamespace.Get;

public class GetDirectionStaffTeamCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetDirectionStaffTeamCommand, List<DirectionStaffTeamDto>>
{
    public override async Task<List<DirectionStaffTeamDto>> ExecuteAsync(GetDirectionStaffTeamCommand command, CancellationToken cancellationToken = default)
    {
        var directionStaffTeamRepository = unitOfWork.Repository<Domain.Entities.DirectionStaffTeam>();

        var entities = await directionStaffTeamRepository.GetAllAsync();

        return entities.Select(x => x.ToDto()).ToList();
    }
}
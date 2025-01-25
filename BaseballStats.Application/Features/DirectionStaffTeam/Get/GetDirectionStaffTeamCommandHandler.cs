using BaseballStats.Application.DTOs;
using BaseballStats.Application.Mappers;
using BaseballStats.Domain.Interfaces.DataAccess;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using BaseballStats.Application.Services;

namespace BaseballStats.Application.Features.DirectionStaffTeam.Get;

public class GetDirectionStaffTeamCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GetDirectionStaffTeamCommand, List<DirectionStaffTeamDto>>
{
    public override async Task<List<DirectionStaffTeamDto>> ExecuteAsync(GetDirectionStaffTeamCommand command, CancellationToken cancellationToken = default)
    {
        var directionStaffRepository = unitOfWork.Repository<Domain.Entities.DirectionStaff>();

        var entities = await directionStaffRepository.GetAllAsync();

        List<DirectionStaffTeamDto> result = new();
        foreach (var entity in entities)
        {
            foreach (var team in entity.TeamsLead)
            {
                result.Add(new DirectionStaffTeamDto { DirectionMemberId = entity.Id, TeamId = team.Id });
            }
        }

        return result;
    }
}
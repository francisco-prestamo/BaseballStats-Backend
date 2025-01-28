using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Application.ResultSets;

namespace BaseballStats.Application.Mappers;

public static class DirectionStaffTeamMapper
{
    public static DirectionStaffTeamDto ToDto(this DirectionStaffTeam directionStaffTeam)
    {
        return new DirectionStaffTeamDto() 
        {
            DirectionMemberId = directionStaffTeam.DirectionStaffId,
            TeamId = directionStaffTeam.TeamId
        };
    }

}
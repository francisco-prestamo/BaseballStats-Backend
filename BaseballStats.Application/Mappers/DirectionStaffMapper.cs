using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Application.ResultSets;

namespace BaseballStats.Application.Mappers;

public static class DirectionStaffMapper
{
    public static DirectionStaffDto ToDto(this DirectionStaff directionStaff)
    {
        return new DirectionStaffDto() 
        {
            Id = directionStaff.Id,
            Name = directionStaff.Name,
            TeamsLead = directionStaff.TeamsLead.Select(x => x.ToDto()).ToList()
        };
    }

}
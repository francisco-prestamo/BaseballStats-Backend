using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class TeamMapper
{
    public static TeamDto ToDto(this Team team)
    {
        return new TeamDto()
        {
            Id = team.Id,
            Name = team.Name,
            Initials = team.Initials,
            Color = team.Color,
            RepresentedEntity = team.RepresentedEntity,
        };
    }

    public static TeamWithDtIdDto ToTeamWithDtIdDto(this Team team)
    {
        return new TeamWithDtIdDto()
        {
            Id = team.Id,
            Name = team.Name,
            Initials = team.Initials,
            Color = team.Color,
            RepresentedEntity = team.RepresentedEntity,
            DtId = team.TechnicalDirectorId
        };
    }
}
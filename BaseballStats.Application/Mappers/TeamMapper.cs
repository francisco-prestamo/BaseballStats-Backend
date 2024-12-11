using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;


namespace BaseballStats.Application.Mappers;

public static class TeamMapper
{
    public static TeamDto ToDto(this Team team)
    {
        return new TeamDto()
        {
            id = team.Id,
            name = team.Name,
            initials = team.Initials,
            color = team.Color,
            representedEntity = team.RepresentedEntity,
        };
    }
}
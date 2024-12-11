using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class TeamWithExtrasMapper
{
    public static TeamWithExtrasDto ToDto(this TeamWithExtras team)
    {
        return new TeamWithExtrasDto()
        {
            id = team.Id,
            name = team.Name,
            initials = team.Initials,
            color = team.Color,
            representedEntity = team.RepresentedEntity,
            winGames = team.winGames,
            loseGames = team.loseGames,
            totalRuns = team.totalRuns
        };
    }
}
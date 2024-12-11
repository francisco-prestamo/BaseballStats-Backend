using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class TeamWithExtrasMapper
{
    public static TeamWithExtrasDto ToDto(this TeamWithExtras team)
    {
        return new TeamWithExtrasDto()
        {
            Id = team.Id,
            Name = team.Name,
            Initials = team.Initials,
            Color = team.Color,
            RepresentedEntity = team.RepresentedEntity,
            WinGames = team.winGames,
            LoseGames = team.loseGames,
            TotalRuns = team.totalRuns
        };
    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.ResultSets;

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
            WinGames = team.WinGames,
            LoseGames = team.LoseGames,
            TotalRuns = team.TotalRuns
        };
    }
}
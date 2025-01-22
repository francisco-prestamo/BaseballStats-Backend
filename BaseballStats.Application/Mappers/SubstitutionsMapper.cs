using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class SubstitutionsMapper
{
    public static GameSubstitutionsDto ToDto(this (List<DTOs.SingleSubstitutionDto>, List<DTOs.SingleSubstitutionDto>) substitutions)
    {
        return new GameSubstitutionsDto()
        {
            Team1Substitutions = substitutions.Item1,
            Team2Substitutions = substitutions.Item2
        };
    }
}
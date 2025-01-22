using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class SubstitutionsMapper
{
    public static SubstitutionsDto ToDto(this (List<DTOs.Substitution>, List<DTOs.Substitution>) substitutions)
    {
        return new SubstitutionsDto()
        {
            Team1Substitutions = substitutions.Item1,
            Team2Substitutions = substitutions.Item2
        };
    }
}
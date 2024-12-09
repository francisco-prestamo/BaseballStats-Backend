using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class SubstitutionsMapper
{
    public static SubstitutionsDto ToDto(this (List<Domain.Entities.Substitution>, List<Domain.Entities.Substitution>) substitutions)
    {
        List<DTOs.Substitution> FormatSubstitutions(List<Domain.Entities.Substitution> substitutions)
        {
            List<DTOs.Substitution> result = new();
            foreach (var substitution in substitutions)
            {
                result.Add(new(substitution.TeamId, substitution.PlayerInId, substitution.PlayerOutId, substitution.Time));
            }
            return result;
        }

        return new SubstitutionsDto()
        {
            Team1Substitutions = FormatSubstitutions(substitutions.Item1),
            Team2Substitutions = FormatSubstitutions(substitutions.Item2)
        };
    }
}
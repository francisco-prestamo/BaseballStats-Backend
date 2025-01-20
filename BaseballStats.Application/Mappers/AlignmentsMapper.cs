using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Application.ResultSets;

namespace BaseballStats.Application.Mappers;

public static class AlignmentsMapper
{
    public static AlignmentsDto ToDto(this (long team1Id, List<Alignment>, long team2Id, List<Alignment>) alignments)
    {
        return new AlignmentsDto() 
        {
            Team1Id = alignments.team1Id,
            Team1Alignment = alignments.Item2.Select(a => a.GetPlayerInPositionDto()).ToList(),
            Team2Id = alignments.team2Id,
            Team2Alignment = alignments.Item4.Select(a => a.GetPlayerInPositionDto()).ToList()
        };
    }
}
using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Mappers;

public static class AlignmentsMapper
{
    public static AlignmentsDto ToDto(this (List<AlignedPlayerInGame>, List<AlignedPlayerInGame>) alignments)
    {
        List<DTOs.PlayerInPosition> FormatAlignment(List<AlignedPlayerInGame> alignment)
        {
            List<DTOs.PlayerInPosition> result = new();
            foreach (var player in alignment)
            {
                result.Add(new(player.PlayerId, player.Position.ToString()));
            }
            return result;
        }

        return new AlignmentsDto()
        {
            Team1Id = alignments.Item1.First().TeamId,
            Team2Id = alignments.Item2.First().TeamId,
            Team1Alignment = FormatAlignment(alignments.Item1),
            Team2Alignment = FormatAlignment(alignments.Item2)
        };
    }
}
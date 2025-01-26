using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Application.ResultSets;

namespace BaseballStats.Application.Mappers;

public static class PlayerInPositionMapper
{
    public static PlayerInPositionDto GetPlayerInPositionDto(this Alignment alignment)
    {
        return new PlayerInPositionDto()
        {
            Player = alignment.GetRegularPlayerDto(),
            Position = alignment.Position.GetDisplayName(),
            Effectiveness = alignment.Effectiveness,  
        };
    }
}
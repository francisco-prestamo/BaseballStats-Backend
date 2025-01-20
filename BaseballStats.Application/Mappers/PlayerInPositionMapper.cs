using BaseballStats.Application.DTOs;
using BaseballStats.Domain.Entities;
using BaseballStats.Application.ResultSets;

namespace BaseballStats.Application.Mappers;

public static class PlayerInPositionMapper
{
    public static PlayerInPositionDto ToDto(this PlayerInPosition playerInPosition, PlayerDto playerDto)
    {
        return new PlayerInPositionDto()
        {
            Player = playerDto,
            Position = playerInPosition.Position
        };
    }

    public static PlayerInPositionDto GetPlayerInPositionDto(this Alignment alignment)
    {
        return new PlayerInPositionDto()
        {
            Player = alignment.GetPlayerDto(),
            Position = alignment.Position,
            Effectiveness = alignment.Effectiveness,  
        };
    }
}
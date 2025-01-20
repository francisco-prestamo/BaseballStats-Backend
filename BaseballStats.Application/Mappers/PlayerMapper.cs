using BaseballStats.Application.DTOs;
using BaseballStats.Application.ResultSets;

namespace BaseballStats.Application.Mappers;

public static class PlayerMapper
{
    public static PlayerDto GetPlayerDto(this Alignment alignment)
    {
        if (alignment.GamesLostNumber != null)
        {
            return new PlayerDto()
            {
                Player = null,
                Pitcher = alignment.GetPitcherDto()
            };
        }
        else
        {
            return new PlayerDto()
            {
                Player = alignment.GetRegularPlayerDto(),
                Pitcher = null
            };
        }
    }
}